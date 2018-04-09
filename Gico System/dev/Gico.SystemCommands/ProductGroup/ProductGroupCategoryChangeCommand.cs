using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupCategoryChangeCommand : Command
    {
        public ProductGroupCategoryChangeCommand()
        {
        }
        public string[] CategoryIds { get; set; }
        public string ProductGroupId { get; set; }
        public string UpdatedUid { get; set; }
    }
}