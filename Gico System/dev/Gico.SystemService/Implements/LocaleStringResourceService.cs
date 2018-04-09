using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemService.Interfaces;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemService.Implements
{
    public class LocaleStringResourceService : ILocaleStringResourceService
    {
        private readonly ICommandSender _commandService;
        private readonly ILocaleStringResourceCacheStorage _localeStringResourceCacheStorage;
        private readonly ILocaleStringResourceRepository _localeStringResourceRepository;

        public LocaleStringResourceService(ICommandSender commandService, ILocaleStringResourceCacheStorage localeStringResourceCacheStorage, ILocaleStringResourceRepository localeStringResourceRepository)
        {
            _commandService = commandService;
            _localeStringResourceCacheStorage = localeStringResourceCacheStorage;
            _localeStringResourceRepository = localeStringResourceRepository;
        }

        #region READ
        public async Task<RLocaleStringResource> GetById(string id)
        {
            return await _localeStringResourceRepository.GetById(id);
        }

        public async Task<RLocaleStringResource[]> GetByLanguageId(string languageId)
        {
            return await _localeStringResourceRepository.GetByLanguageId(languageId);
        }

        public async Task<RLocaleStringResource[]> Search(string LanguageId, string ResourceName, string ResourceValue, RefSqlPaging sqlPaging)
        {
            return await _localeStringResourceRepository.Search( LanguageId, ResourceName, ResourceValue, sqlPaging);
        }
        
        #endregion


        #region WRITE

        public async Task AddToDb(LocaleStringResource locale)
        {
            await _localeStringResourceRepository.Add(locale);
        }

        public async Task ChangeToDb(LocaleStringResource locale)
        {
            await _localeStringResourceRepository.Change(locale);
        }

        #endregion


        #region Command

        
        public async Task<CommandResult> SendCommand(LocaleStringResourceAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(LocaleStringResourceChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        
    }
}
