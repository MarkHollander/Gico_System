using Gico.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Model
{
    public class EmailOrSmsViewModel
    {
        public Int64 NumericalOrder { get; set; }
        public string Id { get; set; }

        public EnumDefine.EmailOrSmsTypeEnum Type { get; set; }
        public string TypeName { get; set; }

        public EnumDefine.EmailOrSmsMessageTypeEnum MessageType { get; set; }
        public string MessageTypeName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public object Model { get; set; }
        public string Template { get; set; }

        public EnumDefine.EmailOrSmsStatusEnum Status { get; set; }
        public string StatusName { get; set; }

        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
        public string VerifyId { get; set; }

        public DateTime? SendDate { get; set; }
    }
}
