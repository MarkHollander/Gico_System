using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface ILocaleStringResourceRepository
    {
        Task<RLocaleStringResource[]> Search( string languageId, string resourceName, string resourceValue, RefSqlPaging sqlPaging);
        Task Add(LocaleStringResource locale);
        Task Change(LocaleStringResource locale);
        Task<RLocaleStringResource> GetById(string id);
        Task<RLocaleStringResource[]> GetByLanguageId(string languageId);
    }
}
