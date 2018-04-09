using System;
using ProtoBuf;

namespace Gico.ReadModels
{
    [ProtoContract]
    public class BaseReadModel
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Code { get; set; }
        [ProtoMember(3)]
        public string LanguageId { get; set; }
        [ProtoMember(4)]
        public string StoreId { get; set; }
        [ProtoMember(5)]
        public DateTime CreatedDateUtc { get; set; }
        [ProtoMember(6)]
        public string CreatedUid { get; set; }
        [ProtoMember(7)]
        public DateTime UpdatedDateUtc { get; set; }
        [ProtoMember(8)]
        public string UpdatedUid { get; set; }
    }
}
