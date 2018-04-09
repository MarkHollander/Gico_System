using Gico.Config;
using Gico.ReadSystemModels;
using ProtoBuf;
using System;

namespace Gico.ReadSystemModels.Product
{
    [ProtoContract]
    public class RVendor_Product_Mapping : BaseReadModel
    {
        [ProtoMember(1)]
        public string ProductId { get; private set; }
        [ProtoMember(2)]
        public string VendorId { get; private set; }
        [ProtoMember(3)]
        public string VendorSku { get; private set; }
        [ProtoMember(4)]
        public decimal OriginalPrice { get; private set; }
        [ProtoMember(5)]
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        [ProtoMember(6)]
        public decimal VAT { get; private set; }
        [ProtoMember(7)]
        public decimal VatEx { get; private set; }
        [ProtoMember(8)]
        public string Barcode { get; private set; }
        [ProtoMember(9)]
        public int IsVisible { get; private set; }

    }
}