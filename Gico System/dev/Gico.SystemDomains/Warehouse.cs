using Gico.Config;
using Gico.Domains;

namespace Gico.SystemDomains
{
    public class Warehouse : BaseDomain
    {
        public string Name { get; set; }
        public string VendorId { get; set; }
        public string Description { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int VillageId { get; set; }
        public int RoadId { get; set; }
        public string AddressDetail { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public EnumDefine.WarehouseTypeEnum Type { get; set; }
        public new EnumDefine.WarehouseStatusEnum Status { get; set; }


    }
}