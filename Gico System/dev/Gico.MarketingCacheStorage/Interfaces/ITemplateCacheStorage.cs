using System.Threading.Tasks;
using Gico.ReadSystemModels.PageBuilder;

namespace Gico.MarketingCacheStorage.Interfaces
{
    public interface ITemplateCacheStorage
    {
        Task AddOrChange(RTemplateCache templateCache);
        Task Remove(string id);
        Task<RTemplateCache> Get(string id);
    }
}