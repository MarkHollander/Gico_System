using Gico.Config;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupWarehouseGetRequest : ProductGroupConditionGetRequest
    {
        public string Keyword { get; set; }
        public EnumDefine.WarehouseTypeEnum Type { get; set; }
        public  EnumDefine.WarehouseStatusEnum Status { get; set; }
        public string VenderId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}