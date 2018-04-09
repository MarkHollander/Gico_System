using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemModels.Request;

namespace Gico.SystemService.Interfaces
{
    public interface IMenuService
    {
        #region Read From DB

        Task<RMenu[]> GetByLanguageId(string languageId);
        Task<RMenu> Get(string id);

        #endregion

        #region Read From Cache

        Task<RMenu[]> GetFromCache(string languageId);
        Task<RMenu> GetFromCache(string languageId, string id);

        #endregion

        #region Write To DB

        Task AddOrChangeMenuBannerMapping(string menuId, string[] bannerIds);
        Task AddMenuBannerMapping(string menuId, string bannerId);
        Task RemoveMenuBannerMapping(string menuId, string bannerId);

        #endregion

        #region SendCommand

        Task<CommandResult> SendCommand(MenuAddCommand command);
        Task<CommandResult> SendCommand(MenuChangeCommand command);
        Task<CommandResult> SendCommand(MenuBannerMappingAddCommand command);
        Task<CommandResult> SendCommand(MenuBannerMappingRemoveCommand command);
        #endregion
    }
}