using Gico.CQRS.Model.Implements;
using Gico.Events;

namespace Gico.OrderEvents.Cache
{
    public class CartCacheRemoveItemEvent : Event
    {
        public string[] CartItemIds { get; set; }
        public string[] CartItemDetailIds { get; set; }
    }
}