using Gico.Config;
using Gico.Events;

namespace Gico.OrderEvents.Cache
{
    public class CartItemCacheAddOrChangeEvent : BaseEvent
    {
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public new EnumDefine.CartStatusEnum Status { get; set; }
    }
}