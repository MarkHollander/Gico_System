using Gico.Models.Request;
using Gico.Config;

namespace Gico.SystemModels.Request.Warehouse
{
    public class WarehouseSearchRequest:BaseRequest
    {
        public string VendorId { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public EnumDefine.WarehouseStatusEnum Status { get; set; }
        public EnumDefine.WarehouseTypeEnum Type { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
