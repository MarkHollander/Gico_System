using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndAppService.Mapping;
using Gico.FrontEndModels;
using Gico.FrontEndModels.Models;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;
using Gico.Config;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.FrontEndAppService.Implements
{
    public class PageAppService : IPageAppService
    {
        protected readonly ILogger<PageAppService> _logger;
        protected readonly ICurrentContext _currentContext;
        protected readonly IMenuService _menuService;
        protected readonly ILocaleStringResourceCacheStorage _localeStringResourceCacheStorage;

        public PageAppService(IMenuService menuService, ILocaleStringResourceCacheStorage localeStringResourceCacheStorage, ICurrentContext currentContext, ILogger<PageAppService> logger)
        {
            _menuService = menuService;
            _localeStringResourceCacheStorage = localeStringResourceCacheStorage;
            _currentContext = currentContext;
            _logger = logger;
        }

        public async Task<PageModel> InitPage()
        {
            try
            {
                PageModel model = new PageModel();
                var menus = await _menuService.GetFromCache(_currentContext.LanguageId);
                model.MenuHeaders = menus?.Where(p => p.Position == EnumDefine.MenuPositionEnum.Header && p.IsPublish).Select(p => p.ToModel()).ToArray();
                model.MenuFooters = menus?.Where(p => p.Position == EnumDefine.MenuPositionEnum.Footer && p.IsPublish).Select(p => p.ToModel()).ToArray();
                model.Seo = new SeoModel();
                model.LanguageId = _currentContext.LanguageId;
                var resources = await _localeStringResourceCacheStorage.Get(model.Resources.Keys.ToArray(), _currentContext.LanguageId);
                model.Resources = resources.Where(p => p != null)
                    .ToDictionary(p => p.ResourceName, p => p.ResourceValue);

                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }


        }
        public async Task<AjaxModel> InitAjax()
        {
            try
            {
                AjaxModel model = new AjaxModel();
                var resources = await _localeStringResourceCacheStorage.Get(model.Resources.Keys.ToArray(), _currentContext.LanguageId);
                model.Resources = resources.Where(p => p != null)
                    .ToDictionary(p => p.ResourceName, p => p.ResourceValue);

                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }


        }

        public async Task<bool> IsAuthenticated()
        {
            return await _currentContext.IsAuthenticated();
        }
    }
}