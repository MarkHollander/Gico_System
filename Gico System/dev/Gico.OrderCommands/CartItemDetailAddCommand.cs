using Gico.CQRS.Model.Implements;

namespace Gico.OrderCommands
{
    public class CartItemDetailAddCommand : Command
    {
        public CartItemDetailAddCommand()
        {
        }

        public CartItemDetailAddCommand(int version) : base(version)
        {
        }

        public string ProductId { get; set; }
        public string Name { get; set; }

        
    }
}