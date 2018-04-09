using Gico.Config;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RMeasureUnit : BaseReadModel
    {
        [ProtoMember(1)]
        public string UnitName { get; set; }
        [ProtoMember(2)]
        public string BaseUnitId { get; set; }
        [ProtoMember(3)]
        public string Ratio { get; set; }
        [ProtoMember(4)]
        public EnumDefine.GiftCodeCampaignStatus UnitStatus { get; set; }
        [ProtoMember(5)]
        public string UnitId { get; set; }
        [ProtoMember(6)]
        public string UnitNameB { get; set; }

    }
}
