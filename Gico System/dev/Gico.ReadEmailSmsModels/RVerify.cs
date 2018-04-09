using System;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadEmailSmsModels
{
    public class RVerify : BaseReadModel
    {
        [ProtoMember(1)]
        public string SaltKey { get; set; }
        [ProtoMember(2)]
        public string SecretKey { get; set; }
        [ProtoMember(3)]
        public DateTime ExpireDate { get; set; }
        [ProtoMember(4)]
        public EnumDefine.VerifyTypeEnum Type { get; set; }
        [ProtoMember(5)]
        public string VerifyCode { get; set; }
        [ProtoMember(6)]
        public string VerifyUrl { get; set; }
        [ProtoMember(7)]
        public string Model { get; set; }
        [ProtoMember(8)]
        public new EnumDefine.VerifyStatusEnum Status { get; set; }
        [ProtoMember(9)]
        public Int64 NumericalOrder { get; set; }

        public T GetModel<T>()
        {
            if (string.IsNullOrEmpty(Model))
            {
                return default(T);
            }
            return Common.Serialize.JsonDeserializeObject<T>(this.Model);
        }
        public bool CheckStatus(EnumDefine.VerifyStatusEnum status)
        {
            return Status.HasFlag(status);
        }
    }
}