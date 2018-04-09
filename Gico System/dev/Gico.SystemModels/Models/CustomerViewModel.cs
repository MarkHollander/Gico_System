using System;
using Gico.Config;
using Gico.Models.Models;

namespace Gico.SystemModels.Models
{
    public class CustomerViewModel : BaseViewModel
    {
        #region Properties
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        // Miễn thuế
        public bool IsTaxExempt { get; set; }
        public int FailedLoginAttempts { get; set; }
        public string LastIpAddress { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public EnumDefine.TwoFactorEnum TwoFactorEnabled { get; set; }
        public string FullName { get; set; }
        public EnumDefine.GenderEnum Gender { get; set; }
        public DateTime? BirthdayValue { get; set; }
        public EnumDefine.CustomerTypeEnum Type { get; set; }
        public new EnumDefine.CustomerStatusEnum Status { get; set; }
        #endregion

        public string GenderName => Gender.ToString();
        public string TypeName => Type.ToString();
        public string StatusName => Status.ToString();
        public string Birthday => BirthdayValue.HasValue ? BirthdayValue.Value.ToString(SystemDefine.DateFormat) : string.Empty;
    }
}