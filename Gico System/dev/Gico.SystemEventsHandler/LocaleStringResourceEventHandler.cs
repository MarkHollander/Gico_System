using System;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemEvents.Cache;

namespace Gico.SystemEventsHandler
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LocaleStringResourceEventHandler : IEventHandler<LocaleStringResourceCacheAddOrChangeEvent>, IEventHandler<LocaleStringResourceCacheRemoveEvent>
    {
        private readonly ILocaleStringResourceCacheStorage _localeStringResourceCacheStorage;

        public LocaleStringResourceEventHandler(ILocaleStringResourceCacheStorage localeStringResourceCacheStorage)
        {
            _localeStringResourceCacheStorage = localeStringResourceCacheStorage;
        }

        public async Task Handle(LocaleStringResourceCacheAddOrChangeEvent mesage)
        {
            try
            {
                RLocaleStringResource localeStringResource = new RLocaleStringResource()
                {
                    ResourceName = mesage.ResourceName,
                    LanguageId = mesage.LanguageId,
                    ResourceValue = mesage.ResourceValue,
                };
                await _localeStringResourceCacheStorage.AddOrChange(localeStringResource);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw;
            }
        }

        public async Task Handle(LocaleStringResourceCacheRemoveEvent mesage)
        {
            try
            {
                await _localeStringResourceCacheStorage.Remove(mesage.ResourceName, mesage.LanguageId);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw;
            }
        }
    }
}