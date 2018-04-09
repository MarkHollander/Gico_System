using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;
namespace Gico.SystemModels.Response
{
    public class LocaleStringResourceGetResponse : BaseResponse
    {
        public LocaleStringResourceViewModel Locale { get; set; }
    }
}
