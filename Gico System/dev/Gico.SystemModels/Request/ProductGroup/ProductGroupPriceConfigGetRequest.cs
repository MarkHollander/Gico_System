using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupPriceConfigGetRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
    }
}