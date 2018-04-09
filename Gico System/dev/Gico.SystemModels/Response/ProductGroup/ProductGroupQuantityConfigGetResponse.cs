using Gico.Models.Response;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupQuantityConfigGetResponse : BaseResponse
    {
        public ProductGroupQuantityConfigModel[] Quantities { get; set; }
    }
}