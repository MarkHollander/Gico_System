using System;
using Gico.Common;
using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class CustomerAddOrChangeRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string AdminComment { get; set; }
        public bool IsTaxExempt { get; set; }
        public string BillingAddressId { get; set; }
        public string ShippingAddressId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public EnumDefine.TwoFactorEnum TwoFactorEnabled { get; set; }
        public string FullName { get; set; }
        public EnumDefine.GenderEnum Gender { get; set; }
        public string Birthday { get; set; }
        public EnumDefine.CustomerTypeEnum Type { get; set; }
        public EnumDefine.CustomerStatusEnum Status { get; set; }
        public int Version { get; set; }

        public DateTime? BirthdayValue => Birthday?.AsDateTimeNullable(SystemDefine.DateFormat);
    }
}