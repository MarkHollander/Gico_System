using Gico.Events;

namespace Gico.OrderEvents.Cache
{
    public class CartItemDetailCacheAddOrChangeEvent : BaseEvent
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
    }
}