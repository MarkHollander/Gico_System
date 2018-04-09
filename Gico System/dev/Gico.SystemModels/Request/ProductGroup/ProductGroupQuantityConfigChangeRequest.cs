using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupQuantityConfigChangeRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public ProductGroupQuantityConfigModel[] Quantities { get; set; }
    }
}