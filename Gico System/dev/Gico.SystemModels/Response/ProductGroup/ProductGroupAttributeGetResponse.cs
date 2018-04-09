using Gico.Models.Response;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupAttributeGetResponse : BaseResponse
    {
        public KeyValueTypeStringModel[] Attributes { get; set; }

    }
}