using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.Common;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Interfaces.PageBuilder;
using Gico.SystemAppService.Mapping;
using Gico.SystemAppService.Mapping.Banner;
using Gico.SystemCommands;
using Gico.SystemModels.Models;
using Gico.SystemModels.Models.Banner;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.Banner;
using Microsoft.Extensions.Logging;

namespace Gico.SystemAppService.Implements.PageBuilder
{
    public class MenuAppService : IMenuAppService
    {
        private readonly ILogger<MenuAppService> _logger;
        private readonly IMenuService _menuService;
        private readonly IBannerService _bannerService;
        private readonly ILanguageService _languageService;
        public MenuAppService(ILogger<MenuAppService> logger, IMenuService menuService, ILanguageService languageService, IBannerService bannerService)
        {
            _logger = logger;
            _menuService = menuService;
            _languageService = languageService;
            _bannerService = bannerService;
        }

        public async Task<MenuGetsResponse> MenuGet(MenuGetsRequest request)
        {
            MenuGetsResponse response = new MenuGetsResponse();
            try
            {
                if (request.Position.AsEnumToInt() > 0)
                {
                    RMenu[] menus = await _menuService.GetByLanguageId(request.LanguageCurrentId);
                    if (menus.Length > 0)
                    {
                        response.Menus = menus?.Where(p => p.Position == request.Position).Select(p => p.ToModel()).OrderBy(p => p.Priority).ThenBy(p=>p.Name).ToArray();
                    }
                }

                RLanguage[] languages = await _languageService.Get();
                if (languages.Length > 0)
                {
                    response.Languages = languages.Select(p => p.ToKeyValueModel()).ToArray();
                }
                response.LanguageDefaultId = "2";
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
        public async Task<MenuGetResponse> MenuGet(MenuGetRequest request)
        {
            MenuGetResponse response = new MenuGetResponse();
            try
            {
                RMenu[] menus = await _menuService.GetByLanguageId(request.LanguageCurrentId);
                if (menus.Length > 0)
                {
                    if (!string.IsNullOrEmpty(request.Id))
                    {
                        RMenu currentMenu = menus.FirstOrDefault(p => p.Id == request.Id);
                        if (currentMenu != null)
                        {
                            menus = RMenu.RemoveCurrentTree(menus, currentMenu);
                        }
                    }
                    response.Parents = menus?.Where(p => p.Position == request.Position).Select(p => p.ToModel()).ToArray();
                }
                RLanguage[] languages = await _languageService.Get();
                if (languages.Length > 0)
                {
                    response.Languages = languages.Select(p => p.ToKeyValueModel()).ToArray();
                }
                if (!string.IsNullOrEmpty(request.Id))
                {
                    RMenu menu = await _menuService.Get(request.Id);
                    if (menu == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.MenuNotFound);
                        return response;
                    }
                    response.Menu = menu.ToModel();
                }
                else
                {
                    response.Menu = new MenuModel()
                    {
                        LanguageId = request.LanguageId,
                        Position = request.Position
                    };
                }
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
        public async Task<MenuAddOrChangeResponse> MenuAddOrChange(MenuAddOrChangeRequest request)
        {
            MenuAddOrChangeResponse response = new MenuAddOrChangeResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Menu.Id))
                {
                    var command = request.Menu.ToAddCommand();
                    var result = await _menuService.SendCommand(command);
                    if (result.IsSucess)
                    {
                        response.SetSucess();
                    }
                    else
                    {
                        response.SetFail(result.Message);
                    }
                }
                else
                {
                    var command = request.Menu.ToChangeCommand(request.Menu.Version);
                    var result = await _menuService.SendCommand(command);
                    if (result.IsSucess)
                    {
                        response.SetSucess();
                    }
                    else
                    {
                        response.SetFail(result.Message);
                    }
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
        public async Task<BannersGetByMenuIdResponse> BannerGetByMenuId(BannerGetByMenuIdRequest request)
        {
            BannersGetByMenuIdResponse response = new BannersGetByMenuIdResponse();
            try
            {
                var banners = await _bannerService.GetBannerByMenuId(request.MenuId);
                if (banners?.Length > 0)
                {
                    var bannerIds = banners.Select(p => p.Id).Distinct().ToArray();
                    var bannerItems = (await _bannerService.GetBannerItemByBannerId(bannerIds)).GroupBy(p => p.BannerId).ToDictionary(p => p.Key, p => p.Select(q => q.ToModel()).ToArray());
                    response.Banners = banners.Select(p => p.ToModel()).ToArray();
                    foreach (var bannerViewModel in response.Banners)
                    {
                        bannerViewModel.BannerItems = bannerItems.ContainsKey(bannerViewModel.Id)
                            ? bannerItems[bannerViewModel.Id]
                            : new BannerItemViewModel[0];
                    }
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
        public async Task<BaseResponse> MenuBannerMappingAdd(MenuBannerMappingAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MenuBannerMappingAddCommand command = request.ToAddBannerCommand();
                var result = await _menuService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
        public async Task<BaseResponse> MenuBannerMappingRemove(MenuBannerMappingRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                MenuBannerMappingRemoveCommand command = request.ToRemoveBannerCommand();
                var result = await _menuService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
    }
}