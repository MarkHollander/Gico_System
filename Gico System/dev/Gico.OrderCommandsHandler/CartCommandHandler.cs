using Gico.CQRS.Service.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.OrderCommands;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderDomains;
using Gico.CQRS.Model.Implements;
using Gico.ExceptionDefine;
using Gico.OrderCacheStorage.Interfaces;
using Gico.ReadCartModels;
using Gico.ShardingConfigService.Interfaces;
using Gico.OrderService.Interfaces;
using Gico.ReadSystemModels;
using Gico.ReadSystemModels.Product;

namespace Gico.OrderCommandsHandler
{
    public class CartCommandHandler : ICommandHandler<CartAddCommand, ICommandResult>, ICommandHandler<CartItemChangeCommand, ICommandResult>, ICommandHandler<AddressSelectedCommand, ICommandResult>
    {
        private EnumDefine.ShardGroupEnum ShardGroup = EnumDefine.ShardGroupEnum.Order;
        private readonly ICartService _cartService;
        private readonly IShardingService _shardingService;
        private readonly IEventSender _eventSender;

        public CartCommandHandler(ICartService cartService, IShardingService shardingService, IEventSender eventSender)
        {
            _cartService = cartService;
            _shardingService = shardingService;
            _eventSender = eventSender;
        }

        public async Task<ICommandResult> Handle(CartAddCommand mesage)
        {
            try
            {
                bool isLockSuccess = await _cartService.CreatingCart(mesage.ClientId);
                if (isLockSuccess)
                {
                    var shard = await _shardingService.GetCurrentWriteShardByRoundRobin(ShardGroup);
                    Cart cart = new Cart(mesage, shard.Id, mesage.Version);
                    CartItem shoppingCartItem = new CartItem(mesage.CartItem);
                    CartItemDetail shoppingCartItemDetail = new CartItemDetail(mesage.CartItemDetail);
                    var newCartItems = cart.Add(shoppingCartItem, shoppingCartItemDetail, mesage.CartItem.Quantity,
                        out var newCartItemDetail);
                    await _cartService.Save(shard.ConnectionString, cart, newCartItems, newCartItemDetail);
                    _eventSender.Add(cart.Events);
                    await _eventSender.Notify();
                    ICommandResult result = new CommandResult()
                    {
                        Message = "",
                        ObjectId = cart.Id,
                        Status = CommandResult.StatusEnum.Sucess,
                    };
                    return result;
                }
                else
                {
                    throw new MessageException(ResourceKey.Cart_IsCreating);
                }

            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
            finally
            {
                await _cartService.CreatedCart(mesage.ClientId);
            }
        }

        public async Task<ICommandResult> Handle(CartItemChangeCommand mesage)
        {
            try
            {
                var shard = await _shardingService.GetShardById(ShardGroup, mesage.ShardId);
                RCart rCart = await _cartService.GetFromDb(shard.ConnectionString, mesage.CartId);
                if (rCart == null)
                {
                    throw new MessageException(ResourceKey.Cart_NotFound);
                }
                Cart cart = new Cart(rCart);
                switch (mesage.Action)
                {
                    case EnumDefine.CartActionEnum.AddNewItem:
                        {
                            CartItem cartItem = new CartItem(mesage);
                            CartItemDetail shoppingCartItemDetail = null;
                            if (mesage.CartItemDetail != null)
                            {
                                shoppingCartItemDetail = new CartItemDetail(mesage.CartItemDetail, mesage.LanguageId, mesage.UpdatedUid);
                            }
                            var newCartItems = cart.Add(cartItem, shoppingCartItemDetail, mesage.Quantity, out var newCartItemDetail);
                            _eventSender.Add(cart.Events);
                            await _cartService.Save(shard.ConnectionString, shard.Id, mesage.CartId, mesage.CartCode, mesage.Version, newCartItems, newCartItemDetail);
                        }
                        break;
                    case EnumDefine.CartActionEnum.RemoveItem:
                        {
                            var cartItemRemoved = cart.Remove(mesage.ProductId, mesage.Quantity, out var shoppingCartItemDetailsRemoved);
                            await _cartService.Remove(shard.ConnectionString, mesage.CartId, mesage.Version,
                                cartItemRemoved?.Select(p => p.Id).ToArray(),
                                shoppingCartItemDetailsRemoved?.Select(p => p.Id).ToArray());
                        };
                        break;
                    case EnumDefine.CartActionEnum.ChangeQuantity:
                        {
                            var changeQuantity = mesage.Quantity;
                            if (changeQuantity > 0)
                            {
                                CartItem cartItem = new CartItem(mesage);
                                CartItemDetail shoppingCartItemDetail = null;
                                if (mesage.CartItemDetail != null)
                                {
                                    shoppingCartItemDetail = new CartItemDetail(mesage.CartItemDetail, mesage.LanguageId, mesage.UpdatedUid);
                                }
                                var newCartItems = cart.Add(cartItem, shoppingCartItemDetail, changeQuantity, out var newCartItemDetail);
                                _eventSender.Add(cart.Events);
                                await _cartService.Save(shard.ConnectionString, shard.Id, mesage.CartId, mesage.CartCode, mesage.Version, newCartItems, newCartItemDetail);
                            }
                            else
                            {
                                var cartItemRemoved = cart.Remove(mesage.ProductId, mesage.Quantity * -1, out var shoppingCartItemDetailsRemoved);
                                await _cartService.Remove(shard.ConnectionString, mesage.CartId, mesage.Version,
                                    cartItemRemoved?.Select(p => p.Id).ToArray(),
                                    shoppingCartItemDetailsRemoved?.Select(p => p.Id).ToArray());
                            }
                        }
                        break;
                }
                await _eventSender.Notify();
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = string.Empty,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(AddressSelectedCommand mesage)
        {
            try
            {
                var shard = await _shardingService.GetShardById(ShardGroup, mesage.ShardId);
                RCart rCart = await _cartService.GetFromDb(shard.ConnectionString, mesage.CartId);
                if (rCart == null)
                {
                    throw new MessageException(ResourceKey.Cart_NotFound);
                }
                if (rCart.Version != mesage.Version)
                {
                    throw new MessageException(ResourceKey.Cart_IsChanged);
                }
                Cart cart = new Cart(rCart);
                RWarehouse[] warehouses = new RWarehouse[0];
                RWarehouse_Product_Mapping[] warehouseProductMappings = new RWarehouse_Product_Mapping[0];
                WorkingTime[] logisticsWorkingTimes = new WorkingTime[0];
                Holiday[] logisticsHolidayTimes = new Holiday[0];
                //tinh ton kho
                var quantityChangedsCancel = cart.AddressSelectedCancel();
                var quantityChanges = cart.AddressSelected(mesage, warehouses, warehouseProductMappings, logisticsWorkingTimes, logisticsHolidayTimes);
                await _eventSender.Notify();
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = string.Empty,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }
    }
}
