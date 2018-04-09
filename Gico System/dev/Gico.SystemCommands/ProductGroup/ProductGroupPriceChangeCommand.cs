using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupPriceChangeCommand : Command
    {
        public string ProductGroupId { get; set; }
        public decimal[] Prices { get; set; }
        public string UpdatedUid { get; set; }
    }
}