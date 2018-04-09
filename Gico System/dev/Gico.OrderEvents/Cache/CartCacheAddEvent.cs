using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.Events;

namespace Gico.OrderEvents.Cache
{
    public class CartCacheAddEvent : BaseVersionedEvent
    {
        public string ClientId { get; set; }
        public new EnumDefine.CartStatusEnum Status { get; set; }
        public CartItemCacheAddOrChangeEvent[] CartItems { get; set; }
        public CartItemDetailCacheAddOrChangeEvent[] CartItemDetails { get; set; }
    }
}
