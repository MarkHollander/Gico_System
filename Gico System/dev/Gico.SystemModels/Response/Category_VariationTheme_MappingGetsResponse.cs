using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class Category_VariationTheme_MappingGetsResponse:BaseResponse
    {
        public CategoryVariationThemeMappingModel[] CategoryVariationThemeMapping { get; set; }
    }
}
