using Gico.Config;

namespace Gico.FrontEndModels.Models
{
    public class VerifyExternalLoginWhenAccountIsExistModel:PageModel
    {
        public string VerifyId { get; set; }
        public string VerifyCode { get; set; }
    }
}