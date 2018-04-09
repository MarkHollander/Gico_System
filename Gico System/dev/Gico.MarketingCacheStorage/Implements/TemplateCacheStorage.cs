using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.MarketingCacheStorage.Interfaces;
using Gico.ReadSystemModels.PageBuilder;

namespace Gico.MarketingCacheStorage.Implements
{
    public class TemplateCacheStorage : CacheStorage, ITemplateCacheStorage
    {
        public TemplateCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }
        #region Key Define
        public static string Key = "TemplateCache";
        #endregion
        public async Task AddOrChange(RTemplateCache templateCache)
        {
            await RedisStorage.HashSet(Key, templateCache.Id, templateCache);
        }

        public async Task Remove(string id)
        {
            await RedisStorage.HashDelete(Key, id);
        }

        public async Task<RTemplateCache> Get(string id)
        {
            return await RedisStorage.HashGet<RTemplateCache>(Key, id);
        }

    }
}
