using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class VariationTheme_AttributeGetResponse:BaseResponse
    {
        public VariationTheme_AttributeModel VariationTheme_Attribute { get; set; }
        public VariationTheme_AttributeGetResponse()
        {
            VariationTheme_Attribute = new VariationTheme_AttributeModel();
        }
    }
}
