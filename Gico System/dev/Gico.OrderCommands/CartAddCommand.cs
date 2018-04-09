using Gico.CQRS.Model.Implements;

namespace Gico.OrderCommands
{
    public class CartAddCommand : Command
    {
        public CartAddCommand()
        {
        }

        public CartAddCommand(int version) : base(version)
        {
        }

        public string Code { get; set; }
        public string LanguageId { get; set; }
        public string StoreId { get; set; }
        public string ClientId { get; set; }
        public string CreatedUid { get; set; }
        public CartItemAddCommand CartItem { get; set; }
        public CartItemDetailAddCommand CartItemDetail { get; set; }

    }
}