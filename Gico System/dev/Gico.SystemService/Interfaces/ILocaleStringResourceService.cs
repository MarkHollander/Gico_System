using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;

namespace Gico.SystemService.Interfaces
{
    public interface ILocaleStringResourceService
    {
        #region READ From DB

        Task<RLocaleStringResource[]> Search(string LanguageId, string ResourceName, string ResourceValue, RefSqlPaging sqlPaging);
        Task<RLocaleStringResource> GetById(string id);
        Task<RLocaleStringResource[]> GetByLanguageId(string languageId);
        #endregion


        #region Write To Db

        Task AddToDb(LocaleStringResource locale);
        Task ChangeToDb(LocaleStringResource locale);

        #endregion


        #region Command

        Task<CommandResult> SendCommand(LocaleStringResourceAddCommand command);
        Task<CommandResult> SendCommand(LocaleStringResourceChangeCommand command);

        #endregion
    }
}
