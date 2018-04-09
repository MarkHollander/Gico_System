using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.OrderCommands
{
    public class CartItemChangeCommand : Command
    {
        public CartItemChangeCommand()
        {
        }

        public CartItemChangeCommand(int version) : base(version)
        {
        }

        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
        public string CartCode { get; set; }
        public string UpdatedUid { get; set; }
        public string LanguageId { get; set; }
        public string StoreId { get; set; }
        public int ShardId { get; set; }
        public EnumDefine.CartActionEnum Action { get; set; }
        public CartItemDetailAddCommand CartItemDetail { get; set; }

        
    }
}