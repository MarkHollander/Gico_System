
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RLanguage:BaseReadModel
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Culture { get; set; }
        [ProtoMember(4)]
        public string UniqueSeoCode { get; set; }
        [ProtoMember(5)]
        public string FlagImageFileName { get; set; }
        [ProtoMember(6)]
        public bool Rtl { get; set; }
        [ProtoMember(7)]
        public bool LimitedToStores { get; set; }
        [ProtoMember(8)]
        public int DefaultCurrencyId { get; set; }
        [ProtoMember(9)]
        public bool Published { get; set; }
        [ProtoMember(10)]
        public int DisplayOrder { get; set; }
    }
}
