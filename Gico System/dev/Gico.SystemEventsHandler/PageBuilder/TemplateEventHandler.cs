using System;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels.Banner;
using Gico.ReadSystemModels.PageBuilder;

using Gico.SystemEvents.Cache.PageBuilder;
using Gico.SystemService.Interfaces.Banner;
using Gico.SystemService.Interfaces.PageBuilder;

namespace Gico.SystemEventsHandler.PageBuilder
{
    public class TemplateEventHandler : IEventHandler<TemplateCacheEvent>, 
        IEventHandler<TemplateCacheRemoveEvent>,
        IEventHandler<BannerCacheEvent>,
        IEventHandler<BannerRemoveEvent>
    {
        private readonly ITemplateService _templateService;
        private readonly IBannerService _bannerService;

        public TemplateEventHandler(ITemplateService templateService, IBannerService bannerService)
        {
            _templateService = templateService;
            _bannerService = bannerService;
        }

        public async Task Handle(TemplateCacheEvent mesage)
        {
            try
            {
                RTemplateCache rTemplateCache = new RTemplateCache(mesage);
                await _templateService.AddTemplateToCache(rTemplateCache);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                e.Data["Input"] = mesage;
                throw;
            }
        }

        public async Task Handle(TemplateCacheRemoveEvent mesage)
        {
            try
            {
                await _templateService.RemoveTemplateToCache(mesage.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                e.Data["Input"] = mesage;
                throw;
            }
        }

        public async Task Handle(BannerCacheEvent mesage)
        {
            try
            {
                RBannerCache bannerCache=new RBannerCache(mesage);
                await _bannerService.AddToCache(bannerCache);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                e.Data["Input"] = mesage;
                throw;
            }
        }

        public async Task Handle(BannerRemoveEvent mesage)
        {
            try
            {
                await _bannerService.RemoveToCache(mesage.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                e.Data["Input"] = mesage;
                throw;
            }
        }
    }
}