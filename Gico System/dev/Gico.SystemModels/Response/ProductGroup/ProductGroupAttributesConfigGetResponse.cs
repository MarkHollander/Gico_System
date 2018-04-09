using Gico.Models.Response;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupAttributesConfigGetResponse : BaseResponse
    {
        public ProductAttributeViewModel[] Attributes { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}