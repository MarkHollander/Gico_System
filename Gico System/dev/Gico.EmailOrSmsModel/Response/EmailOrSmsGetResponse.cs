using Gico.Config;
using Gico.EmailOrSmsModel.Model;
using Gico.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Response
{
    public class EmailOrSmsGetResponse : BaseResponse
    {
        public EmailOrSmsGetResponse()
        {
            MessageTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.EmailOrSmsMessageTypeEnum), false);
        }

        public EmailOrSmsViewModel EmailSms { get; set; }
        public KeyValueTypeIntModel[] MessageTypes { get; private set; }
    }
}
