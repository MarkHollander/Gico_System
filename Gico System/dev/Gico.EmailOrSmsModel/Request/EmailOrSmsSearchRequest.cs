using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Request
{
    public class EmailOrSmsSearchRequest : BaseRequest
    {
        public EnumDefine.EmailOrSmsTypeEnum Type { get; set; }
        public EnumDefine.EmailOrSmsMessageTypeEnum MessageType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public EnumDefine.EmailOrSmsStatusEnum Status { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? SendDate { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
