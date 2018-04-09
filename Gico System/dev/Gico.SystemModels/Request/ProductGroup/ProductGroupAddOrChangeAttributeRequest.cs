using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupAddOrChangeAttributeRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string AttributeId { get; set; }
        public string[] AttributeValueIds { get; set; }
    }
}