using Gico.Models.Response;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupAttributeValueGetResponse : BaseResponse
    {
        public KeyValueTypeStringModel[] AttributeValues { get; set; }

    }
}