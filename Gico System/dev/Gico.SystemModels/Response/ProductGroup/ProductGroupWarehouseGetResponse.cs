using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.Warehouse;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupWarehouseGetResponse : BaseResponse
    {
        public ProductGroupWarehouseGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.WarehouseStatusEnum));
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.WarehouseTypeEnum));
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ProductGroupWarehouseViewModel[] Warehouses { get; set; }

        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public KeyValueTypeIntModel[] Types { get; private set; }
    }
}