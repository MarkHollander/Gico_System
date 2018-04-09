using System;
using Gico.Common;
using Gico.Config;

namespace Gico.FrontEndModels.Models
{
    public class RegisterViewModel : PageModel
    {
        public RegisterViewModel()
        {
        }

        public RegisterViewModel(PageModel model) : base(model)
        {
        }

        public string FullName { get; set; }
        public string EmailOrMobile { get; set; }

        public string Email
        {
            get
            {
                string emailOrMobile = EmailOrMobile;
                if (Common.Common.IsEmail(ref emailOrMobile))
                {
                    EmailOrMobile = emailOrMobile;
                    return emailOrMobile;
                }
                return string.Empty;
            }
        }

        public string Mobile
        {
            get
            {
                string emailOrMobile = EmailOrMobile;
                if (Common.Common.IsMobile(ref emailOrMobile))
                {
                    EmailOrMobile = emailOrMobile;
                    return emailOrMobile;
                }
                return string.Empty;
            }
        }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public EnumDefine.GenderEnum Gender { get; set; }
        public string Birthday { get; set; }

        public DateTime? BirthdayValue => Birthday.AsDateTimeNullable(SystemDefine.DateFormat);

        public bool IsReceiveNewletter { get; set; }
        public string Captcha { get; set; }

    }
}