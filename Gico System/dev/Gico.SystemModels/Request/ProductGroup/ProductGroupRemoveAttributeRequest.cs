using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupRemoveAttributeRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string AttributeId { get; set; }
    }
}