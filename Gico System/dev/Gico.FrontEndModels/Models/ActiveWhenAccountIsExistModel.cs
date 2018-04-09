namespace Gico.FrontEndModels.Models
{
    public class ActiveWhenAccountIsExistModel : PageModel
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
        public string ActiveCode { get; set; }
    }
}