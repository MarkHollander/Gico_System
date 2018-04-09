using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupCategoryChangeRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string[] CategoryIds { get; set; }
    }
}
