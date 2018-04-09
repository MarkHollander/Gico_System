using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RCurrency
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public decimal Rate { get; set; }
        [ProtoMember(4)]
        public string DisplayLocale { get; set; }
        [ProtoMember(5)]
        public string CustomFormatting { get; set; }
        [ProtoMember(6)]
        public bool LimitedToStores { get; set; }
        [ProtoMember(7)]
        public bool Published { get; set; }
        [ProtoMember(8)]
        public int DisplayOrder { get; set; }
        [ProtoMember(9)]
        public int RoundingTypeId { get; set; }
    }
}