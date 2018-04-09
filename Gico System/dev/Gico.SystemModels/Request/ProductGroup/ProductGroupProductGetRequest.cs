using Gico.Config;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupProductGetRequest : ProductGroupConditionGetRequest
    {
        public string Keyword { get; set; }
        public EnumDefine.ProductType Type { get; set; }
        public EnumDefine.ProductStatus Status { get; set; }
        public string VenderId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}