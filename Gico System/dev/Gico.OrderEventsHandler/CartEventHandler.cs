using Gico.CQRS.Service.Interfaces;
using Gico.OrderEvents.Cache;
using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.ReadCartModels;
using Gico.OrderCacheStorage.Interfaces;

namespace Gico.OrderEventsHandler
{
    public class CartEventHandler : IEventHandler<CartCacheAddEvent>
    {
        private readonly ICartCacheStorage _cartCacheStorage;

        public CartEventHandler(ICartCacheStorage cartCacheStorage)
        {
            _cartCacheStorage = cartCacheStorage;
        }

        public async Task Handle(CartCacheAddEvent mesage)
        {
            try
            {
                RCart cart = new RCart()
                {
                    Id = mesage.Id,
                    LanguageId = mesage.LanguageId,
                    ShardId = mesage.ShardId,
                    Version = mesage.Version,
                    ClientId = mesage.ClientId,
                    Code = mesage.Code,
                    CreatedDateUtc = mesage.CreatedDateUtc,
                    CreatedUid = mesage.CreatedUid,
                    Status = mesage.Status,
                    StoreId = mesage.StoreId,
                    UpdatedUid = mesage.UpdatedUid,
                    UpdatedDateUtc = mesage.UpdatedDateUtc,
                    CartItems = mesage.CartItems?.Select(p => new RCartItem()
                    {
                        LanguageId = mesage.LanguageId,
                        Status = p.Status,
                        ShardId = mesage.ShardId,
                        Code = p.Code,
                        CreatedUid = mesage.CreatedUid,
                        Id = p.Id,
                        Version = mesage.Version,
                        StoreId = mesage.StoreId,
                        UpdatedUid = mesage.UpdatedUid,
                        CreatedDateUtc = mesage.CreatedDateUtc,
                        UpdatedDateUtc = mesage.UpdatedDateUtc,
                        ProductId = p.ProductId,
                        Price = p.Price,
                    }).ToArray(),
                    CartItemDetails = mesage.CartItemDetails?.Select(p => new RCartItemDetail()
                    {
                        LanguageId = mesage.LanguageId,
                        ShardId = mesage.ShardId,
                        Code = p.Code,
                        CreatedUid = mesage.CreatedUid,
                        Id = p.Id,
                        Version = mesage.Version,
                        StoreId = mesage.StoreId,
                        UpdatedUid = mesage.UpdatedUid,
                        Name = p.Name,
                        ProductId = p.ProductId,
                        CreatedDateUtc = mesage.CreatedDateUtc,
                        UpdatedDateUtc = mesage.UpdatedDateUtc,
                    }).ToArray()
                };
                //var t1 = Common.Serialize.ProtoBufSerialize(cart);
                //var t2 = Common.Serialize.ProtoBufDeserialize<RCart>(t1);
                await _cartCacheStorage.Add(cart);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw e;
            }
        }
    }
}
