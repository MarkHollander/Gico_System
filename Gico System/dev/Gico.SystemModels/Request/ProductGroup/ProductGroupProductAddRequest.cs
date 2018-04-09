using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupProductAddRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string ProductId { get; set; }
    }
}