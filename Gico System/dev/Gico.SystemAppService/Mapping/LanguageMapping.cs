using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Mapping
{
    public static class LanguageMapping
    {
        public static KeyValueTypeStringModel ToKeyValueModel(this RLanguage language)
        {
            if (language == null) return null;
            return new KeyValueTypeStringModel()
            {
                Value = language.Id,
                Checked = false,
                Text = language.Name
            };
        }
        public static LanguageViewModel ToModel(this RLanguage request)
        {
            if (request == null) return null;
            return new LanguageViewModel()
            {
                
                Id = request.Id,
                Name = request.Name,
                Culture = request.Culture,
                UniqueSeoCode=request.UniqueSeoCode,
                FlagImageFileName=request.FlagImageFileName,
                Published=request.Published,
                DisplayOrder=request.DisplayOrder
            };
        }
        public static LanguageAddCommand ToCommand(this LanguageAddOrChangeRequest request, string ip)
        {
            if (request == null) return null;
            return new LanguageAddCommand()
            {
                
                Name = request.Name,
                Culture = request.Culture,
                UniqueSeoCode = request.UniqueSeoCode,
                FlagImageFileName = request.FlagImageFileName,
                Published = request.Published,
                DisplayOrder = request.DisplayOrder,
                LastIpAddress = ip


            };
        }
        public static LanguageChangeCommand ToCommand(this LanguageAddOrChangeRequest request, string ip, int version)
        {
            if (request == null) return null;
            return new LanguageChangeCommand()
            {
                Id = request.Id.GetValueOrDefault(),
                Name = request.Name,
                Culture = request.Culture,
                UniqueSeoCode = request.UniqueSeoCode,
                FlagImageFileName = request.FlagImageFileName,
                Published = request.Published,
                DisplayOrder = request.DisplayOrder,
                Version = version,
                LastIpAddress = ip

            };
        }

    }
}