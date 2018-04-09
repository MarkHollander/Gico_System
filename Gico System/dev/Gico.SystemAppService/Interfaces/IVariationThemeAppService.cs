using System.Threading.Tasks;
using Gico.SystemModels.Response;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Interfaces
{
    public interface IVariationThemeAppService
    {
        Task<VariationThemeGetResponse> VariationThemeGet(VariationThemeGetRequest request);
        Task<VariationTheme_AttributeGetResponse> VariationTheme_AttributeGet(VariationTheme_AttributeGetRequest request);

        Task<Category_VariationTheme_MappingGetsResponse> Category_VariationTheme_MappingGets(Category_VariationTheme_MappingGetsRequest request);
        Task<Category_VariationTheme_MappingAddResponse> Category_VariationTheme_MappingAdd(Category_VariationTheme_MappingAddRequest request);
        Task<Category_VariationTheme_Mapping_RemoveResponse> Remove(Category_VariationTheme_Mapping_RemoveRequest request);
       

    }

}
