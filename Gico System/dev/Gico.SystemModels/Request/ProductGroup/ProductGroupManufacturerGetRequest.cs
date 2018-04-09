using Gico.Config;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupManufacturerGetRequest : ProductGroupVendorGetRequest
    {
        public new EnumDefine.StatusEnum Status { get; set; }
    }
}