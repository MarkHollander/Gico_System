using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class VariationThemeGetResponse : BaseResponse
    {
       
        public KeyValueTypeIntModel[] VariationThemes { get; set; }

        public VariationThemeGetResponse()
        {

        }
    }
}
