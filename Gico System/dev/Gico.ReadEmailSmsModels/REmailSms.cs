using System;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadEmailSmsModels
{
    public class REmailSms : BaseReadModel
    {
        [ProtoMember(1)]
        public EnumDefine.EmailOrSmsTypeEnum Type { get; set; }
        [ProtoMember(2)]
        public EnumDefine.EmailOrSmsMessageTypeEnum MessageType { get; set; }
        [ProtoMember(3)]
        public string PhoneNumber { get; set; }
        [ProtoMember(4)]
        public string Email { get; set; }
        [ProtoMember(5)]
        public string Title { get; set; }
        [ProtoMember(6)]
        public string Content { get; set; }
        [ProtoMember(7)]
        public object Model { get; set; }
        [ProtoMember(8)]
        public string Template { get; set; }
        [ProtoMember(9)]
        public new EnumDefine.EmailOrSmsStatusEnum Status { get; set; }
        [ProtoMember(10)]
        public string VerifyId { get; set; }
        [ProtoMember(11)]
        public RVerify Verify { get; set; }
        [ProtoMember(12)]
        public DateTime? SendDate { get; set; }
        [ProtoMember(13)]
        public Int64 NumericalOrder { get; set; }
    }
}
