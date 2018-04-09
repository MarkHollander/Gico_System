using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.ExceptionDefine;
using Gico.ReadSystemModels;
using Gico.SystemService.Interfaces;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemService.Implements
{
    public class LanguageService : ILanguageService
    {
        private readonly ICommandSender _commandService;
        private readonly ILanguageCacheStorage _languageCacheStorage;
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ICommandSender commandService,ILanguageCacheStorage languageCacheStorage, ILanguageRepository languageRepository)
        {
            _commandService = commandService;
            _languageCacheStorage = languageCacheStorage;
            _languageRepository = languageRepository;
        }
        public async Task<RLanguage[]> Get()
        {
            return await _languageRepository.Get();
        }

        public async Task<RLanguage> Get(string id)
        {
            return await _languageRepository.Get(id);
        }

        public async Task EventCacheInit(RLanguage rLanguage)
        {
            Language language = new Language();
            language.Init(rLanguage);
            //await _languageRepository.NotifyEvent(language);
        }

        public async Task<RLanguage[]> Search(string name, RefSqlPaging paging)
        {
            return await _languageRepository.Search(name, paging);
        }

        #region write
        public async Task ChangeToDb(Language language)
        {
            bool isChanged = await _languageRepository.Change(language);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.language_NotChanged);
            }
        }
        public async Task AddToDb(Language Language)
        {
            await _languageRepository.Add(Language);
        }
        #endregion

        #region read from db
        public async Task<RLanguage> GetFromDb(string id)
        {
            return await _languageRepository.Get(id);
        }
        #endregion
        #region Command

        
        public async Task<CommandResult> SendCommand(LanguageAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(LanguageChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion
    }
}