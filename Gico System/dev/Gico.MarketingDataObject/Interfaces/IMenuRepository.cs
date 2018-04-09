using System.Data;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.MarketingDataObject.Interfaces
{
    public interface IMenuRepository
    {
        Task<RMenu[]> GetByLanguageId(string languageId);
        Task<RMenu> Get(string id);

        Task Add(Menu menu);
        Task Change(Menu menu);
        Task Remove(Menu menu);
        Task AddOrChangeMenuBannerMapping(DataTable dataTable, string menuId);
        Task AddMenuBannerMapping(string menuId, string bannerId);
        Task RemoveMenuBannerMapping(string menuId, string bannerId);
    }
}