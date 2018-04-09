using Gico.Models.Response;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupAttributeConfigGetResponse : BaseResponse
    {
        public ProductAttributeViewModel Attribute { get; set; }

    }
}