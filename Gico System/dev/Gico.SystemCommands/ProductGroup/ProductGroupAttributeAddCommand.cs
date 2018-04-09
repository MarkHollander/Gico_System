using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupAttributeAddCommand : Command
    {
        public string ProductGroupId { get; set; }
        public string AttributeId { get; set; }
        public string[] AttributeValueIds { get; set; }
        public string UpdatedUid { get; set; }
    }
}