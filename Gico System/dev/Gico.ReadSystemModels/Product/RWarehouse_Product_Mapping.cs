using Gico.Config;
using Gico.ReadSystemModels;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Gico.ReadSystemModels.Product
{
    [ProtoContract]
    public class RWarehouse_Product_Mapping : BaseReadModel
    {
        [ProtoMember(1)]
        public string VendorProductId { get; private set; }
        [ProtoMember(2)]
        public string WarehouseId { get; private set; }
        [ProtoMember(3)]
        public int Quantity { get; private set; }
        [ProtoMember(4)]
        public decimal SellPrice { get; private set; }
        [ProtoMember(5)]
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        [ProtoMember(6)]
        public int SafetyStock { get; private set; }
        [ProtoMember(7)]
        public DateTime? StartDateTimeUtc { get; private set; }
        [ProtoMember(8)]
        public DateTime? EndDateTimeUtc { get; private set; }
        [ProtoMember(9)]
        public int QtyReserved { get; private set; }
        [ProtoMember(10)]
        public string ProductId { get; private set; }
        public int QuantityCanUse => Quantity - SafetyStock;
        public Dictionary<int, Tuple<int, int>> PickupAndShippingTimeByWard { get; set; }

    }
}