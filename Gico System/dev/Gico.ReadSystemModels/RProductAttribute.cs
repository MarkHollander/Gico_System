using System;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RProductAttribute : BaseReadModel
    {
        [ProtoMember(1)]
        public string AttributeId { get; set; }
        [ProtoMember(2)]
        public string AttributeName { get; set; }
        [ProtoMember(3)]
        public EnumDefine.StatusEnum AttributeStatus { get; set; }
        [ProtoMember(4)]
        public DateTime CreatedOnUtc { get; set; }
        [ProtoMember(5)]
        public DateTime UpdatedOnUtc { get; set; }
        [ProtoMember(6)]
        public string CreatedUserId { get; set; }
        [ProtoMember(7)]
        public string UpdatedUserId { get; set; }
    }

    [ProtoContract]
    public class RProductAttributeValue : BaseReadModel
    {
        [ProtoMember(1)]
        public string AttributeValueId { get; set; }
        [ProtoMember(2)]
        public string AttributeId { get; set; }
        [ProtoMember(3)]
        public string Value { get; set; }
        [ProtoMember(4)]
        public int UnitId { get; set; }
        [ProtoMember(5)]
        public EnumDefine.StatusEnum AttributeValueStatus { get; set; }
        [ProtoMember(6)]
        public DateTime CreatedOnUtc { get; set; }
        [ProtoMember(7)]
        public DateTime UpdatedOnUtc { get; set; }
        [ProtoMember(8)]
        public string CreatedUserId { get; set; }
        [ProtoMember(9)]
        public string UpdatedUserId { get; set; }
        [ProtoMember(10)]
        public int DisplayOrder { get; set; }
    }
}