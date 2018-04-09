using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.Config;

namespace Gico.SystemService.Interfaces
{
    public interface IVariationThemeService
    {
        Task<RVariationTheme[]> Get();
        Task<RVariationTheme_Attribute[]> Get(int Id);

        Task<RCategory_VariationTheme_Mapping[]> Get(string categoryId);

        Task<RVariationTheme> GetVariationThemeById(int Id);

        Task<CommandResult> SendCommand(Category_VariationTheme_Mapping_AddCommand command);
        Task<CommandResult> SendCommand(Category_VariationTheme_Mapping_RemoveCommand command);
        Task AddToDb(Category_VariationTheme_Mapping category_VariationTheme_Mapping);
        Task RemoveToDb(Category_VariationTheme_Mapping category_VariationTheme_Mapping);

        
    }
}
