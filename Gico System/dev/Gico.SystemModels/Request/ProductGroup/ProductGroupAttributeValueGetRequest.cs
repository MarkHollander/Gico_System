using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupAttributeValueGetRequest : BaseRequest
    {
        public string AttributeId { get; set; }
        public string Keyword { get; set; }

    }
}