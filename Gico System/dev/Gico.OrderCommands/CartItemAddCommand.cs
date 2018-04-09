using System;
using Gico.CQRS.Model.Implements;

namespace Gico.OrderCommands
{
    public class CartItemAddCommand : Command
    {
        public CartItemAddCommand()
        {
        }

        public CartItemAddCommand(int version) : base(version)
        {
        }

        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CreatedUid { get; set; }
        public string LanguageId { get; set; }
        public string StoreId { get; set; }
    }
}
