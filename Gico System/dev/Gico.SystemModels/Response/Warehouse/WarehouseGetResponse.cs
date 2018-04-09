using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;
using Gico.SystemModels.Models.Warehouse;

namespace Gico.SystemModels.Response.Warehouse
{
    public class WarehouseGetResponse : BaseResponse
    {
        public WarehouseGetResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.WarehouseTypeEnum), false);
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.WarehouseStatusEnum), false);
        }
        public WarehouseViewModel Warehouse { get; set; }
        public KeyValueTypeIntModel[] Types { get; private set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }

    }
}
