using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupVendorChangeRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string[] CategoryIds { get; set; }
    }
}