using System;
using System.Linq;
using Gico.Config;
using Gico.Domains;
using Gico.OrderCommands;
using Gico.OrderEvents.Cache;
using Gico.ReadCartModels;
using Gico.ReadSystemModels;

namespace Gico.OrderDomains
{
    public class CartItem : BaseDomain
    {
        public CartItem()
        {

        }
        public CartItem(RCartItem rCartItem)
        {
            Id = rCartItem.Id;
            ShardId = rCartItem.ShardId;
            CreatedDateUtc = rCartItem.CreatedDateUtc;
            UpdatedDateUtc = rCartItem.UpdatedDateUtc;
            CreatedUid = rCartItem.CreatedUid;
            UpdatedUid = rCartItem.UpdatedUid;
            LanguageId = rCartItem.LanguageId;
            StoreId = rCartItem.StoreId;
            Status = rCartItem.Status;
            ProductId = rCartItem.ProductId;
            Price = rCartItem.Price;

        }
        #region Publish

        public CartItem(CartItemAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            ProductId = command.ProductId;
            Price = command.Price;
            Status = EnumDefine.CartStatusEnum.New;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid;
            UpdatedUid = command.CreatedUid;
            LanguageId = command.LanguageId;
            StoreId = command.StoreId;
        }

        public CartItem(CartItemChangeCommand command)
        {
            Id = Common.Common.GenerateGuid();
            ProductId = command.ProductId;
            Price = command.Price;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.UpdatedUid;
            UpdatedUid = command.UpdatedUid;
            LanguageId = command.LanguageId;
            StoreId = command.StoreId;
            Status = EnumDefine.CartStatusEnum.New;

        }

        public void Remove()
        {
            Status = EnumDefine.CartStatusEnum.Remove;
        }

        public void SetStatus(EnumDefine.CartStatusEnum status)
        {
            Status = status;
        }

        public bool CheckStatus(EnumDefine.CartStatusEnum status)
        {
            return Status == status;
        }

        public void SetShippingTime(DateTime pickupTime, int shippingMinuteConfig, WorkingTime[] logisticsWorkingTimes, Holiday[] logisticsHolidayTimes)
        {
            PickupTime = pickupTime;
            ShippingTime = PickupTime.Value.AddMinutes(shippingMinuteConfig);
            int i = -1;
            while (true)
            {
                i++;
                if (i > 10)
                {
                    break;
                }
                ShippingTime = ShippingTime.Value.AddDays(i);
                var dayOfWeek = ShippingTime.Value.DayOfWeek;
                WorkingTime workingDay = logisticsWorkingTimes.FirstOrDefault(p => p.DayOfWeek == dayOfWeek);
                if (workingDay == null)
                {
                    ShippingTime = ShippingTime.Value.Date;
                    continue;
                }
                // ReSharper disable once AccessToModifiedClosure
                Holiday holidayDay = logisticsHolidayTimes.FirstOrDefault(p => p.Day.Date == ShippingTime.Value.Date);
                if (holidayDay != null)
                {
                    var workingTime =
                        holidayDay.WorkingTimes.FirstOrDefault(p => p.Item2 - p.Item1 > shippingMinuteConfig);
                    if (workingTime == null)
                    {
                        continue;
                    }
                    ShippingTime = ShippingTime.Value.Date.AddMinutes(workingTime.Item1 + shippingMinuteConfig);
                }
                else
                {
                    ShippingTime = ShippingTime.Value.Date.AddMinutes(workingDay.Times[0].Item1 + shippingMinuteConfig);
                }
            }
        }

        public void SetPoCode(int identity)
        {
            PoCode = string.Format("{0}{1}", Code, identity.ToString().PadLeft(3));
        }

        public void WarehouseSelected(RWarehouse warehouse)
        {
            WarehouseId = warehouse.WarehouseId;
            SetStatus(EnumDefine.CartStatusEnum.EnoughInventory);
        }

        public void WarehouseSelectedCancel()
        {
            WarehouseId = string.Empty;
            SetStatus(EnumDefine.CartStatusEnum.New);
            PickupTime = null;
            ShippingTime = null;
        }

        #endregion

        #region Event

        public void CreateAddItemToCartEvent()
        {
            var @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }

        #endregion

        public string ProductId { get; private set; }
        public decimal Price { get; private set; }
        public new EnumDefine.CartStatusEnum Status { get; private set; }
        public string WarehouseId { get; private set; }
        public int ProductType { get; private set; }
        public bool StraightDelivery { get; private set; }
        public string PoCode { get; private set; }
        public DateTime? PickupTime { get; private set; }
        public DateTime? ShippingTime { get; private set; }
        #region Convert

        public CartItemCacheAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new CartItemCacheAddOrChangeEvent()
            {
                Id = this.Id,
                Status = this.Status,
                Price = this.Price,
                ProductId = this.ProductId,
                LanguageId = this.LanguageId,
                ShardId = this.ShardId,
                Code = this.Code,
                CreatedDateUtc = this.CreatedDateUtc,
                CreatedUid = this.CreatedUid,
                StoreId = this.StoreId,
                UpdatedDateUtc = this.UpdatedDateUtc,
                UpdatedUid = this.UpdatedUid,
            };
        }
        #endregion
    }
}