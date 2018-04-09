using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupManufacturerGetResponse : BaseResponse
    {
        public ProductGroupManufacturerGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.StatusEnum));
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ManufacturerViewModel[] Manufacturers { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }

    }
}