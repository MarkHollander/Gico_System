using System.Threading.Tasks;
using Gico.ReadSystemModels;

namespace Gico.SystemCacheStorage.Interfaces
{
    public interface IMenuCacheStorage
    {
        Task<RMenu[]> Get(string languageId);
        Task<RMenu> Get(string languageId,string id);
        Task<bool> AddOrChange(RMenu menu);
        Task<bool> Remove(string languageId, string id);
    }
}