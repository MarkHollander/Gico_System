using System.Data;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemService.Interfaces;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.CQRS.Model.Implements;
using Gico.MarketingDataObject.Interfaces;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemModels.Request;

namespace Gico.SystemService.Implements
{
    public class MenuService : IMenuService
    {
        private readonly ICommandSender _commandService;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuCacheStorage _menuCacheStorage;

        public MenuService(IMenuRepository menuRepository, IMenuCacheStorage menuCacheStorage, ICommandSender commandService)
        {
            _menuRepository = menuRepository;
            _menuCacheStorage = menuCacheStorage;
            _commandService = commandService;
        }

        #region Read From DB

        public async Task<RMenu[]> GetByLanguageId(string languageId)
        {
            return await _menuRepository.GetByLanguageId(languageId);
        }
        public async Task<RMenu> Get(string id)
        {
            return await _menuRepository.Get(id);
        }

        #endregion

        #region Read From Cache

        public async Task<RMenu[]> GetFromCache(string languageId)
        {
            return await _menuCacheStorage.Get(languageId);
        }
        public async Task<RMenu> GetFromCache(string languageId, string id)
        {
            return await _menuCacheStorage.Get(languageId, id);
        }

        #endregion

        #region Write To DB

        public async Task AddOrChangeMenuBannerMapping(string menuId, string[] bannerIds)
        {
            DataTable dataTable = new DataTable("Menu_Banner_Mapping");
            dataTable.Columns.Add("MenuId", typeof(string));
            dataTable.Columns.Add("BannerId", typeof(string));
            foreach (var bannerId in bannerIds)
            {
                DataRow dr = dataTable.NewRow();
                dr["MenuId"] = menuId;
                dr["BannerId"] = bannerId;
                dataTable.Rows.Add(dataTable);
            }
            await _menuRepository.AddOrChangeMenuBannerMapping(dataTable, menuId);
        }

        public async Task AddMenuBannerMapping(string menuId, string bannerId)
        {
            await _menuRepository.AddMenuBannerMapping(menuId, bannerId);
        }

        public async Task RemoveMenuBannerMapping(string menuId, string bannerId)
        {
            await _menuRepository.RemoveMenuBannerMapping(menuId, bannerId);
        }

        #endregion

        #region SendCommand

        public async Task<CommandResult> SendCommand(MenuAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(MenuChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(MenuBannerMappingAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(MenuBannerMappingRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

    }
}
