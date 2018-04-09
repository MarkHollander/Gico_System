using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class ProductAttributeSearchRequest : BaseRequest
    {
        public ProductAttributeSearchRequest()
        {
            AttributeId = string.Empty;
            AttributeName = string.Empty;
        }

        public string AttributeId { get; set; }
        public string AttributeName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ProductAttributeValueSearchRequest : BaseRequest
    {
        public ProductAttributeValueSearchRequest()
        {
            AttributeValueId = string.Empty;
        }

        public string AttributeValueId { get; set; }
        public string AttributeId { get; set; }
        public string Value { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}