using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupWarehouseAddRequest : BaseRequest
    {
        public string ProductGroupId { get; set; }
        public string WarehouseId { get; set; }
    }
}