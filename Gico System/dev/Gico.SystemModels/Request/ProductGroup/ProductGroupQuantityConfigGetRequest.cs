using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupQuantityConfigGetRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
    }
}