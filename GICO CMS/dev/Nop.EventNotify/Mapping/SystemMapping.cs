using Gico.SystemEvents;
using Gico.SystemEvents.Cache;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;

namespace Nop.EventNotify.Mapping
{
    public static class SystemMapping
    {
        public static CurrencyCacheAddOrChangeEvent ToAddOrChangeEvent(this Currency input)
        {
            if (input == null)
            {
                return null;
            }
            return new CurrencyCacheAddOrChangeEvent()
            {
                Name = input.Name,
                CustomFormatting = input.CustomFormatting,
                DisplayLocale = input.DisplayLocale,
                DisplayOrder = input.DisplayOrder,
                Id = input.Id.ToString(),
                LimitedToStores = input.LimitedToStores,
                Published = input.Published,
                Rate = input.Rate,
                RoundingTypeId = input.RoundingTypeId,
            };
        }
        public static CurrencyCacheRemoveEvent ToRemoveEvent(this Currency input)
        {
            if (input == null)
            {
                return null;
            }
            return new CurrencyCacheRemoveEvent()
            {
                Id = input.Id.ToString(),
            };
        }
        public static LanguageCacheEvent ToEvent(this Language input)
        {
            if (input == null)
            {
                return null;
            }
            return new LanguageCacheEvent()
            {
                Name = input.Name,
                DisplayOrder = input.DisplayOrder,
                Id = input.Id.ToString(),
                LimitedToStores = input.LimitedToStores,
                Published = input.Published,
                Culture = input.LanguageCulture,
                DefaultCurrencyId = input.DefaultCurrencyId,
                FlagImageFileName = input.FlagImageFileName,
                Rtl = input.Rtl,
                UniqueSeoCode = input.UniqueSeoCode
            };
        }
        public static LocaleStringResourceCacheEvent ToEvent(this LocaleStringResource input)
        {
            if (input == null)
            {
                return null;
            }
            return new LocaleStringResourceCacheEvent()
            {
                LanguageId = input.LanguageId,
                MenuId = string.Empty,
                ResourceName = input.ResourceName,
                ResourceValue = input.ResourceValue
            };
        }

    }
}