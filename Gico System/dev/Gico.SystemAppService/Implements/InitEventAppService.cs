using System.Threading.Tasks;
using Gico.SystemAppService.Interfaces;
using Gico.SystemService.Interfaces;

namespace Gico.SystemAppService.Implements
{
    public class InitEventAppService : IInitEventAppService
    {
        private readonly ILanguageService _languageService;

        public InitEventAppService(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public async Task LanguageInit()
        {
            var languages = await _languageService.Get();
            if (languages.Length > 0)
            {
                foreach (var language in languages)
                {
                    await _languageService.EventCacheInit(language);
                }
            }
        }
    }
}