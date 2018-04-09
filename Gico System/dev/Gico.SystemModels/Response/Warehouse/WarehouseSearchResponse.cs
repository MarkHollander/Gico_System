using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;
using Gico.SystemModels.Models.Warehouse;

namespace Gico.SystemModels.Response.Warehouse
{
    public class WarehouseSearchResponse:BaseResponse
    {
        public WarehouseSearchResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.WarehouseTypeEnum));
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.WarehouseStatusEnum));
        }
        public KeyValueTypeIntModel[] Types { get; private set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public WarehouseViewModel[] Warehouses { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }


    }
}
