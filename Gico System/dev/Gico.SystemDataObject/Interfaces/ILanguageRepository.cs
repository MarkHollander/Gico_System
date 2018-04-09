using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface ILanguageRepository
    {
        #region read

        Task<RLanguage[]> Get();
        Task<RLanguage> Get(string id);
        Task<RLanguage[]> Search(string name, RefSqlPaging paging);

        #endregion

        #region write

        Task Add(Language language);
        Task<bool> Change(Language language);

        #endregion
    }
}