using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupQuantityChangeCommand : Command
    {
        public string ProductGroupId { get; set; }
        public int[] Quantities { get; set; }
        public string UpdatedUid { get; set; }
    }
}