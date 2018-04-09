using System;
using System.Net;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Config;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndModels.Models;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.PageBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEndAppService.Implements
{
    public class HomeAppService : PageAppService, IHomeAppService
    {
        private readonly ITemplateService _templateService;
        public HomeAppService(IMenuService menuService,
            ILocaleStringResourceCacheStorage localeStringResourceCacheStorage,
            ILogger<HomeAppService> logger,
            ICurrentContext currentContext, ITemplateService templateService) : base(menuService, localeStringResourceCacheStorage, currentContext, logger)
        {
            _templateService = templateService;
        }
        public async Task<HomeViewModel> Get(string tmpCode)
        {
            try
            {
                HomeViewModel model = new HomeViewModel(await InitPage());
                var template = await _templateService.GetTemplateToCache(tmpCode);
                if (template == null || template.Status != EnumDefine.CommonStatusEnum.Active)
                {
                    model.AddMessage(ResourceKey.Template_NotFound);
                    model.HttpResponseCode = StatusCodes.Status404NotFound;
                    return model;
                }
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }

        }



    }
}