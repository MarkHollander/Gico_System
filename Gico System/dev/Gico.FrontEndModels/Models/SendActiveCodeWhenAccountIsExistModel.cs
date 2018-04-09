using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.FrontEndModels.Models
{
    public class SendActiveCodeWhenAccountIsExistModel : PageModel
    {
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
    }
}
