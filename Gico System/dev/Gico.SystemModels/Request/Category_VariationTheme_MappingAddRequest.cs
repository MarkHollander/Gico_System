using Gico.Config;
using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class Category_VariationTheme_MappingAddRequest : BaseRequest
    {
        public Category_VariationTheme_MappingModel Category_VariationTheme_Mapping { get; set; }
      
    }
}
