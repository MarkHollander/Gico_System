using System;
using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.Config;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IVariationThemeRepository
    {
        Task<RVariationTheme[]> Get();
        Task<RVariationTheme_Attribute[]> Get(int Id);

        Task<RCategory_VariationTheme_Mapping[]> Get(string categoryId);

        Task<RVariationTheme> GetVariationThemeById(int Id);

        Task Add(Category_VariationTheme_Mapping category_VariationTheme_Mapping);

        Task Remove(Category_VariationTheme_Mapping category_VariationTheme_Mapping);


    }
}
