using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupVendorAddRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string VendorId { get; set; }
    }
}