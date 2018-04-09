using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Mapping
{
    public static class LocaleStringResourceMapping
    {
        public static LocaleStringResourceViewModel ToModel(this RLocaleStringResource locale)
        {
            if (locale == null)
            {
                return null;
            }
            return new LocaleStringResourceViewModel()
            {
                Id = locale.Id,
                LanguageId = locale.LanguageId,
                ResourceName = locale.ResourceName,
                ResourceValue = locale.ResourceValue,
                LanguageName = locale.LanguageName,
            };
        }

        //public static KeyValueTypeStringModel ToKeyValueTypeStringModel(RLanguage lang)
        //{
        //    if (lang == null)
        //    {
        //        return null;
        //    }
        //    return new KeyValueTypeStringModel()
        //    {
        //        Value = lang.LanguageId,
        //        Text = lang.Name
        //    };
        //}

        public static LocaleStringResourceAddCommand ToCommand(this LocaleStringResourceAddRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new LocaleStringResourceAddCommand()
            {
                LanguageId = request.LanguageId,
                ResourceName = request.ResourceName,
                ResourceValue = request.ResourceValue,
                Id = Common.Common.GenerateGuid()
            };
        }

        public static LocaleStringResourceChangeCommand ToCommand(this LocaleStringResourceChangeRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new LocaleStringResourceChangeCommand()
            {
                LanguageId = request.LanguageId,
                ResourceName = request.ResourceName,
                ResourceValue = request.ResourceValue,
                Id = request.Id
            };
        }
    }
}
