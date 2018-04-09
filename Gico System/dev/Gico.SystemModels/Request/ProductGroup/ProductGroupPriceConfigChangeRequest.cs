using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupPriceConfigChangeRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public ProductGroupPriceConfigModel[] Prices { get; set; }
    }
}