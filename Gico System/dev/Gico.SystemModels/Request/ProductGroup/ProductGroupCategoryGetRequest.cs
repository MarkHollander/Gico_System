using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupCategoryGetRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
    }
}