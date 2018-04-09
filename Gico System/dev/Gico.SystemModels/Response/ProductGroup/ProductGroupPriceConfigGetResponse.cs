using Gico.Models.Response;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupPriceConfigGetResponse : BaseResponse
    {
        public ProductGroupPriceConfigModel[] Prices { get; set; }
    }
}