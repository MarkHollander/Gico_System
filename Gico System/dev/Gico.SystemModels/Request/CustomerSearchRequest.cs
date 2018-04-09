using System;
using Gico.Common;
using Gico.Config;

namespace Gico.SystemModels.Request
{
    public class CustomerSearchRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string FromBirthday { get; set; }
        public string ToBirthDay { get; set; }
        public EnumDefine.CustomerTypeEnum Type { get; set; }
        public EnumDefine.CustomerStatusEnum Status { get; set; }

        public DateTime? FromBirthdayValue => FromBirthday.AsDateTimeNullable(SystemDefine.DateFormat);
        public DateTime? ToBirthDayValue => ToBirthDay.AsDateTimeNullable(SystemDefine.DateFormat);
        public int PageIndex { get; set; }
        public int PageSize  { get; set; }
    }
}