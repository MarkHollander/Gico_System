using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupVendorGetRequest : ProductGroupConditionGetRequest
    {
        public string Keyword { get; set; }
        public EnumDefine.VendorStatusEnum Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    
}