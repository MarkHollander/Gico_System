using Gico.Config;
using System;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RVendor : BaseReadModel
    {
        #region Properties
        [ProtoMember(1)]
        public string Email { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string CompanyName { get; set; }
        [ProtoMember(4)]
        public string Description { get; set; }
        [ProtoMember(5)]
        public string Logo { get; set; }
        [ProtoMember(6)]
        public string Phone { get; set; }
        [ProtoMember(7)]
        public string Fax { get; set; }
        [ProtoMember(8)]
        public string Website { get; set; }
        [ProtoMember(9)]
        public EnumDefine.VendorTypeEnum VendorType { get; set; }
        [ProtoMember(10)]
        public new EnumDefine.VendorStatusEnum Status { get; set; }
        #endregion
    }
}