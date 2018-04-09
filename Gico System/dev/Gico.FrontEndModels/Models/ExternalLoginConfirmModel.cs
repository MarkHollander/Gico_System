using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;

namespace Gico.FrontEndModels.Models
{
    public class ExternalLoginConfirmViewModel : PageModel
    {
        public EnumDefine.CutomerExternalLoginProviderEnum LoginProvider { get; set; }
        public string Email { get; set; }
    }
}
