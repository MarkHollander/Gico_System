using Gico.SystemCommands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.CQRS.Model.Implements;
using Gico.MarketingDataObject.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.Banner;
using Gico.ReadSystemModels.Banner;

namespace Gico.SystemCommandsHandler
{
    public class MenuCommandHandler : ICommandHandler<MenuAddCommand, ICommandResult>,
        ICommandHandler<MenuChangeCommand, ICommandResult>,
        ICommandHandler<MenuBannerMappingAddCommand, ICommandResult>,
        ICommandHandler<MenuBannerMappingRemoveCommand, ICommandResult>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuService _menuService;
        private readonly IBannerService _bannerService;

        public MenuCommandHandler(IMenuRepository menuRepository, IMenuService menuService, IBannerService bannerService)
        {
            _menuRepository = menuRepository;
            _menuService = menuService;
            _bannerService = bannerService;
        }

        public async Task<ICommandResult> Handle(MenuAddCommand message)
        {
            try
            {
                Menu menu = new Menu();
                menu.Add(message);
                await _menuRepository.Add(menu);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = menu.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(MenuChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RMenu rMenu = await _menuService.Get(message.Id);
                if (rMenu == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Menu not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                Menu menu = new Menu(rMenu);
                menu.Change(message);
                await _menuRepository.Change(menu);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = menu.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(MenuBannerMappingAddCommand message)
        {
            try
            {
                ICommandResult result;
                RMenu rMenu = await _menuService.Get(message.MenuId);
                if (rMenu == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Menu not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                RBanner[] rbanners = await _bannerService.GetBannerByMenuId(rMenu.Id);
                Menu menu = new Menu(rMenu, rbanners);
                RBanner banner = await _bannerService.GetBannerById(message.BannerId);
                var bannerAdd = menu.AddBanner(banner);
                await _menuService.AddMenuBannerMapping(menu.Id, bannerAdd.Id);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = menu.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(MenuBannerMappingRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RMenu rMenu = await _menuService.Get(message.MenuId);
                if (rMenu == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Menu not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                RBanner[] rbanners = await _bannerService.GetBannerByMenuId(rMenu.Id);
                Menu menu = new Menu(rMenu, rbanners);
                var banner = menu.RemoveBanner(message.BannerId);
                await _menuService.RemoveMenuBannerMapping(menu.Id, banner.Id);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = menu.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }
    }
}
