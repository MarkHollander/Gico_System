using Gico.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Model
{
    public class VerifyViewModel
    {
        public Int64 NumericalOrder { get; set; }
        public string Id { get; set; }
        public string SaltKey { get; set; }
        public string SecretKey { get; set; }
        public DateTime ExpireDate { get; set; }
        public EnumDefine.VerifyTypeEnum Type { get; set; }
        public string TypeName { get; set; }
        public string VerifyCode { get; set; }
        public string VerifyUrl { get; set; }
        public string Model { get; set; }
        public EnumDefine.VerifyStatusEnum Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
    }
}
