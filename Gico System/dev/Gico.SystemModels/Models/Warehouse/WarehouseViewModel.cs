using System;
using Gico.Config;
using Gico.Models.Models;


namespace Gico.SystemModels.Models.Warehouse
{
    public class WarehouseViewModel : BaseViewModel
    {
        #region Properties
        public string Name { get; set; }
        public string AddressDetail { get; set; }
        public string PhoneNumber { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public int RoadId { get; set; }
        public string RoadName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public new EnumDefine.WarehouseStatusEnum Status { get; set; }
        public EnumDefine.WarehouseTypeEnum Type { get; set; }
        public string StatusName => Status.ToString();
        public string TypeName => Type.ToString();


        #endregion
    }

    public class ProductGroupWarehouseViewModel : WarehouseViewModel
    {
        public bool IsAdd { get; set; }
    }

}
