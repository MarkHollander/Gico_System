using System;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemEvents.Cache;

namespace Gico.SystemEventsHandler
{
    public class CurrencyEventHandler : IEventHandler<CurrencyCacheAddOrChangeEvent>, IEventHandler<CurrencyCacheRemoveEvent>
    {
        private readonly ICurrencyCacheStorage _currencyCacheStorage;

        public CurrencyEventHandler(ICurrencyCacheStorage currencyCacheStorage)
        {
            _currencyCacheStorage = currencyCacheStorage;
        }

        public async Task Handle(CurrencyCacheAddOrChangeEvent mesage)
        {
            try
            {
                RCurrency currency = new RCurrency()
                {
                    Id = mesage.Id,
                    CustomFormatting = mesage.CustomFormatting,
                    DisplayLocale = mesage.DisplayLocale,
                    DisplayOrder = mesage.DisplayOrder,
                    LimitedToStores = mesage.LimitedToStores,
                    Name = mesage.Name,
                    Published = mesage.Published,
                    Rate = mesage.Rate,
                    RoundingTypeId = mesage.RoundingTypeId,
                };
                await _currencyCacheStorage.AddOrChange(currency);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw e;
            }
        }

        public async Task Handle(CurrencyCacheRemoveEvent mesage)
        {
            try
            {
                await _currencyCacheStorage.Remove(mesage.Id);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw e;
            }
        }
    }
}
