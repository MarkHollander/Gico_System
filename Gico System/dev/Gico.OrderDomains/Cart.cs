using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;
using Gico.OrderCommands;
using Gico.OrderEvents.Cache;
using Gico.ReadCartModels;
using Gico.ReadSystemModels;
using Gico.ReadSystemModels.Product;

namespace Gico.OrderDomains
{
    public class Cart : BaseDomain, IVersioned
    {
        public Cart(int version)
        {
            Version = version;
        }

        public Cart(RCart cart)
        {
            Id = cart.Id;
            ShardId = cart.ShardId;
            Code = cart.Code;
            CreatedDateUtc = cart.CreatedDateUtc;
            UpdatedDateUtc = cart.UpdatedDateUtc;
            CreatedUid = cart.CreatedUid;
            UpdatedUid = cart.UpdatedUid;
            LanguageId = cart.LanguageId;
            StoreId = cart.StoreId;
            ClientId = cart.ClientId;
            Status = cart.Status;
            Version = cart.Version;
            CartItems = cart.CartItems.Select(p => new CartItem(p)).ToList();
            CartItemDetails = cart.CartItemDetails?.ToDictionary(p => p.ProductId, p => new CartItemDetail(p));
        }

        #region Publish Function

        public Cart(CartAddCommand command, int shardId, int version) : this(version)
        {
            Id = Common.Common.GenerateGuid();
            Code = command.Code;
            CartItems = new List<CartItem>();
            CartItemDetails = new ConcurrentDictionary<string, CartItemDetail>();
            ClientId = command.ClientId;
            CreatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid;
            LanguageId = command.LanguageId;
            StoreId = command.StoreId;
            Status = EnumDefine.CartStatusEnum.New;
            ShardId = shardId;
        }

        public CartItem[] Add(CartItem shoppingCartItem, CartItemDetail shoppingCartItemDetail, int quantity, out CartItemDetail newCartItemDetail)
        {
            newCartItemDetail = null;
            IList<CartItem> cartItems = new List<CartItem>();
            for (int i = 0; i < quantity; i++)
            {
                CartItems.Add(shoppingCartItem);
                cartItems.Add(shoppingCartItem);
            }
            if (newCartItemDetail != null)
            {
                if (!CartItemDetails.ContainsKey(shoppingCartItemDetail.ProductId))
                {
                    CartItemDetails.Add(shoppingCartItemDetail.ProductId, shoppingCartItemDetail);
                    newCartItemDetail = shoppingCartItemDetail;
                }
            }

            CreateAddEvent();
            return cartItems.ToArray();
        }

        public IList<CartItem> Remove(string productId, int quantity, out IList<CartItemDetail> shoppingCartItemDetailsRemoved)
        {
            IList<CartItem> itemRemoves = new List<CartItem>();
            shoppingCartItemDetailsRemoved = new List<CartItemDetail>();
            bool isRemove = false;
            for (int i = 0; i < quantity; i++)
            {
                var item = CartItems.FirstOrDefault(p => p.ProductId == productId);
                if (item != null)
                {
                    CartItems.Remove(item);
                    isRemove = true;
                    itemRemoves.Add(item);
                }
            }
            if (isRemove)
            {
                var item = CartItems.FirstOrDefault(p => p.ProductId == productId);
                if (item == null)
                {
                    if (CartItemDetails.ContainsKey(productId))
                    {
                        shoppingCartItemDetailsRemoved.Add(CartItemDetails[productId]);
                        CartItemDetails.Remove(productId);
                    }

                }
            }
            RemoveItemEvent(itemRemoves, shoppingCartItemDetailsRemoved);
            return itemRemoves;
        }

        public WarehouseQuantityChange[] AddressSelectedCancel()
        {
            Dictionary<string, WarehouseQuantityChange> quantityChanges = new Dictionary<string, WarehouseQuantityChange>();
            foreach (var cartItem in CartItems)
            {
                cartItem.WarehouseSelectedCancel();
                if (!string.IsNullOrEmpty(cartItem.WarehouseId) &&
                    cartItem.CheckStatus(EnumDefine.CartStatusEnum.EnoughInventory))
                {
                    string key = $"ProductId:{cartItem.ProductId}, WarehouseId:{cartItem.WarehouseId}, Price:{cartItem.Price}";
                    if (!quantityChanges.ContainsKey(key))
                    {
                        quantityChanges.Add(key, new WarehouseQuantityChange(cartItem.WarehouseId, cartItem.ProductId, cartItem.Price, 1));
                    }
                    else
                    {
                        quantityChanges[key].QuantityUp(1);
                    }
                }
            }
            return quantityChanges.Values.ToArray();
        }

        public WarehouseQuantityChange[] AddressSelected(AddressSelectedCommand command, RWarehouse[] warehouses, RWarehouse_Product_Mapping[] warehouseProductMappings,
            WorkingTime[] logisticsWorkingTimes, Holiday[] logisticsHolidayTimes)
        {
            var quantityChanges = ValidateQuantityInStock(command, warehouses, warehouseProductMappings, logisticsWorkingTimes, logisticsHolidayTimes);
            CreatePo();
            return quantityChanges;
        }

        private void CreatePo()
        {
            var poGroups = CartItems.GroupBy(p => new { p.WarehouseId, p.ProductType });
            int i = 0;
            foreach (var poGroup in poGroups)
            {
                foreach (var item in poGroup)
                {
                    item.SetPoCode(i);
                }
            }
        }

        private WarehouseQuantityChange[] ValidateQuantityInStock(AddressSelectedCommand command, RWarehouse[] warehouses, RWarehouse_Product_Mapping[] warehouseProductMappings,
            WorkingTime[] logisticsWorkingTimes, Holiday[] logisticsHolidayTimes)
        {
            IList<WarehouseQuantityChange> quantityChanges = new List<WarehouseQuantityChange>();
            var cartItemsGroups = CartItems.GroupBy(p => $"ProductId:{p.ProductId}, SellPrice:{p.Price}");
            var warehouseProductMappingsGroups = warehouseProductMappings.GroupBy(p => $"ProductId:{p.Id}, SellPrice:{p.SellPrice}").ToDictionary(p => p.Key, p => p.ToArray());
            foreach (var cartItemsGroup in cartItemsGroups)
            {
                int quantityBuy = cartItemsGroup.Count();
                EnumDefine.CartStatusEnum status;
                RWarehouse_Product_Mapping[] warehouseProductMappingsGroup = null;
                if (warehouseProductMappingsGroups.ContainsKey(cartItemsGroup.Key))
                {
                    warehouseProductMappingsGroup = warehouseProductMappingsGroups[cartItemsGroup.Key];
                    int quantityInStock = warehouseProductMappingsGroup.Sum(p => p.QuantityCanUse);
                    status = quantityBuy > quantityInStock ? EnumDefine.CartStatusEnum.NotEnoughQuantity : EnumDefine.CartStatusEnum.EnoughInventory;
                }
                else
                {
                    status = EnumDefine.CartStatusEnum.NotInStock;
                }
                if (status == EnumDefine.CartStatusEnum.EnoughInventory && warehouseProductMappingsGroup != null)
                {
                    foreach (var warehouseProductMapping in warehouseProductMappingsGroup)
                    {
                        bool isSetShippingTime = false;
                        foreach (var cartItem in cartItemsGroup)
                        {
                            var warehouseSelected = warehouses.FirstOrDefault(p =>
                                p.WarehouseId == warehouseProductMapping.WarehouseId);
                            isSetShippingTime = GetShippingTimes(cartItem, warehouseSelected, warehouseProductMapping, command, logisticsWorkingTimes, logisticsHolidayTimes);
                            if (isSetShippingTime)
                            {
                                cartItem.WarehouseSelected(warehouseSelected);
                            }
                        }
                        if (!isSetShippingTime)
                        {
                            continue;
                        }
                        int quantityChange = 0;
                        if (quantityBuy <= warehouseProductMapping.QuantityCanUse)
                        {
                            quantityChange = quantityBuy;
                            quantityChanges.Add(new WarehouseQuantityChange(warehouseProductMapping.WarehouseId, warehouseProductMapping.ProductId, warehouseProductMapping.SellPrice, quantityChange));
                            break;
                        }
                        else
                        {
                            quantityChange = warehouseProductMapping.QuantityCanUse;
                            quantityChanges.Add(new WarehouseQuantityChange(warehouseProductMapping.WarehouseId, warehouseProductMapping.ProductId, warehouseProductMapping.SellPrice, quantityChange));
                        }

                    }

                }
            }
            return quantityChanges.ToArray();
        }

        private bool GetShippingTimes(CartItem cartItem, RWarehouse warehouse, RWarehouse_Product_Mapping warehouseProductMapping, AddressSelectedCommand command, WorkingTime[] logisticsWorkingTimes, Holiday[] logisticsHolidayTimes)
        {
            if (warehouseProductMapping.PickupAndShippingTimeByWard.ContainsKey(command.WardId))
            {
                cartItem.SetStatus(EnumDefine.CartStatusEnum.NotShippingSupport);
                return false;
            }
            DateTime dtNow = Extensions.GetCurrentDateUtc();
            DateTime shippingTime = dtNow;
            int i = -1;
            while (true)
            {
                i++;
                if (i > 10)
                {
                    break;
                }
                shippingTime = shippingTime.AddDays(i);
                var dayOfWeek = shippingTime.DayOfWeek;
                WorkingTime workingTime = warehouse.WorkingTimes.FirstOrDefault(p => p.DayOfWeek == dayOfWeek);
                if (workingTime == null)
                {
                    shippingTime = shippingTime.Date;
                    continue;
                }
                // ReSharper disable once AccessToModifiedClosure
                Holiday holidayDay = warehouse.HolidayTimes.FirstOrDefault(p => p.Day.Date == shippingTime.Date);
                int pickupMinute = 0;
                var productShippingTimeConfig = warehouseProductMapping.PickupAndShippingTimeByWard[command.WardId];
                if (holidayDay != null)
                {
                    // ReSharper disable once AccessToModifiedClosure
                    var holidayDayWorkingTimes = holidayDay.WorkingTimes.FirstOrDefault(p => shippingTime.Minute + productShippingTimeConfig.Item1 < p.Item2);
                    if (holidayDayWorkingTimes == null)
                    {
                        shippingTime = shippingTime.Date;
                        continue;
                    }
                    pickupMinute = holidayDayWorkingTimes.Item1;
                }
                else
                {
                    pickupMinute = workingTime.Times[0].Item1;
                }
                cartItem.SetShippingTime(shippingTime.AddMinutes(pickupMinute), productShippingTimeConfig.Item2, logisticsWorkingTimes, logisticsHolidayTimes);
                return true;
            }
            cartItem.SetStatus(EnumDefine.CartStatusEnum.IsVenderHoliday);
            return false;
        }

        #endregion

        #region Events

        public void CreateAddEvent()
        {
            var @event = this.ToAddOrChangeEvent();
            AddEvent(@event);
        }

        public void RemoveItemEvent(IList<CartItem> cartItems, IList<CartItemDetail> shoppingCartItemDetails)
        {
            CartCacheRemoveItemEvent @event = new CartCacheRemoveItemEvent()
            {
                CartItemIds = cartItems?.Select(p => p.Id).ToArray(),
                CartItemDetailIds = shoppingCartItemDetails?.Select(p => p.Id).ToArray()
            };
            AddEvent(@event);
        }

        #endregion

        public string ClientId { get; private set; }
        public new EnumDefine.CartStatusEnum Status { get; private set; }
        public List<CartItem> CartItems { get; private set; }
        public Dictionary<string, CartItem[]> Pos => CartItems.GroupBy(p => p.PoCode).ToDictionary(p => p.Key, p => p.ToArray());
        public IDictionary<string, CartItemDetail> CartItemDetails { get; private set; }
        public int Version { get; }

        #region Convert

        public CartCacheAddEvent ToAddOrChangeEvent()
        {
            return new CartCacheAddEvent()
            {
                Id = this.Id,
                Status = this.Status,
                Code = this.Code,
                Version = this.Version + 1,
                ClientId = this.ClientId,
                ShardId = this.ShardId,
                LanguageId = this.LanguageId,
                CreatedUid = this.CreatedUid,
                StoreId = this.StoreId,
                UpdatedDateUtc = this.UpdatedDateUtc,
                UpdatedUid = this.UpdatedUid,
                CreatedDateUtc = this.CreatedDateUtc,
                CartItems = CartItems?.Select(p => p.ToAddOrChangeEvent()).ToArray(),
                CartItemDetails = CartItemDetails?.Select(p => p.Value.ToAddOrChangeEvent()).ToArray(),

            };
        }

        #endregion

    }
}