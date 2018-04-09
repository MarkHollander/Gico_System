using System;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemEvents.Cache;

namespace Gico.SystemEventsHandler
{
    public class LanguageEventHandler : IEventHandler<LanguageCacheAddOrChangeEvent>, IEventHandler<LanguageCacheRemoveEvent>
    {
        private readonly ILanguageCacheStorage _languageCacheStorage;

        public LanguageEventHandler(ILanguageCacheStorage languageCacheStorage)
        {
            _languageCacheStorage = languageCacheStorage;
        }

        public async Task Handle(LanguageCacheAddOrChangeEvent mesage)
        {
            try
            {
                RLanguage rLanguage = new RLanguage()
                {
                    Id = mesage.Id,
                    DisplayOrder = mesage.DisplayOrder,
                    LimitedToStores = mesage.LimitedToStores,
                    Name = mesage.Name,
                    Published = mesage.Published,
                    Culture = mesage.Culture,
                    DefaultCurrencyId = mesage.DefaultCurrencyId,
                    FlagImageFileName = mesage.FlagImageFileName,
                    Rtl = mesage.Rtl,
                    UniqueSeoCode = mesage.UniqueSeoCode
                };
                await _languageCacheStorage.AddOrChange(rLanguage);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw e;
            }
        }

        public async Task Handle(LanguageCacheRemoveEvent mesage)
        {
            try
            {
                await _languageCacheStorage.Remove(mesage.Id);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw e;
            }
        }
    }
}