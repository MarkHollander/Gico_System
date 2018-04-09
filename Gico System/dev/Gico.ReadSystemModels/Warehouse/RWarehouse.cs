using Gico.Config;
using System;
using ProtoBuf;

namespace Gico.ReadSystemModels.Warehouse
{
    [ProtoContract]
    public class RWarehouse : BaseReadModel
    {
        #region Properties
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string VendorId { get; set; }
        [ProtoMember(3)]
        public string Description { get; set; }
        [ProtoMember(4)]
        public int ProvinceId { get; set; }
        [ProtoMember(5)]
        public int DistrictId { get; set; }
        [ProtoMember(6)]
        public int VillageId { get; set; }
        [ProtoMember(7)]
        public int RoadId { get; set; }
        [ProtoMember(8)]
        public string AddressDetail { get; set; }
        [ProtoMember(9)]
        public string PhoneNumber { get; set; }
        [ProtoMember(10)]
        public EnumDefine.WarehouseTypeEnum Type { get; set; }
        [ProtoMember(11)]
        public string Email { get; set; }
        [ProtoMember(12)]
        public new EnumDefine.WarehouseStatusEnum Status { get; set; }
        //[ProtoMember(13)]
        //public string VendorName { get; set; }

        #endregion


    }
}
