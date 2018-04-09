using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupManufacturerAddRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string ManufacturerId { get; set; }
    }
}