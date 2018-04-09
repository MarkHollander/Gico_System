using System.Threading.Tasks;
using Gico.ReadSystemModels.Banner;
using Gico.ReadSystemModels.PageBuilder;

namespace Gico.MarketingCacheStorage.Interfaces
{
    public interface IComponentCacheStorage
    {
        Task AddToCache(RComponentCache component);
        Task RemoveToCache(string id);
        Task<RComponentCache> GetFromCache(string id);
    }
}