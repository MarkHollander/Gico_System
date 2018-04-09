using Gico.Config;
using Gico.EmailOrSmsModel.Model;
using Gico.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Response
{
    public class EmailOrSmsSearchResponse : BaseResponse
    {
        public EmailOrSmsSearchResponse()
        {
            EmailStatuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.EmailOrSmsStatusEnum));
            EmailTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.EmailOrSmsTypeEnum));
            EmailMessageTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.EmailOrSmsMessageTypeEnum));
        }

        public EmailOrSmsViewModel[] EmailSmses { get; set; }
        public KeyValueTypeIntModel[] EmailStatuses { get; }
        public KeyValueTypeIntModel[] EmailTypes { get; }
        public KeyValueTypeIntModel[] EmailMessageTypes { get; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
