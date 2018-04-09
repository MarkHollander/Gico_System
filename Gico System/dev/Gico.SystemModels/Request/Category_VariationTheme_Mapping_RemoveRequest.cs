using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class Category_VariationTheme_Mapping_RemoveRequest : BaseRequest
    {
        //public CategoryVariationThemeMappingModel CategoryVariationThemeMapping { get; set; }

        public int VariationThemeId { get; set; }
        public string CategoryId { get; set; }

    }
}
