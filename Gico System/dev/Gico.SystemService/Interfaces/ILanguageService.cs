using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;

namespace Gico.SystemService.Interfaces
{
    public interface ILanguageService
    {
        Task<RLanguage[]> Get();
        Task<RLanguage> Get(string id);
        Task<RLanguage[]> Search(string name, RefSqlPaging paging);
        Task EventCacheInit(RLanguage language);
        Task<RLanguage> GetFromDb(string id);

        #region write
        Task AddToDb(Language language);
        Task ChangeToDb(Language language);
        #endregion
        #region Command

        
        Task<CommandResult> SendCommand(LanguageAddCommand command);
        Task<CommandResult> SendCommand(LanguageChangeCommand command);

        #endregion
    }
}