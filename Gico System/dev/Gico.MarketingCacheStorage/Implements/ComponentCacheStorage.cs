using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.MarketingCacheStorage.Interfaces;
using Gico.ReadSystemModels.Banner;
using Gico.ReadSystemModels.PageBuilder;

namespace Gico.MarketingCacheStorage.Implements
{
    public class ComponentCacheStorage : CacheStorage, IComponentCacheStorage
    {
        public ComponentCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }
        #region Key Define
        public static string Key = "ComponentCache";
        #endregion
        public async Task AddToCache(RComponentCache component)
        {
            await RedisStorage.HashSet(Key, component.Id, component);
        }

        public async Task RemoveToCache(string id)
        {
            await RedisStorage.HashDelete(Key, id);
        }

        public async Task<RComponentCache> GetFromCache(string id)
        {
            return await RedisStorage.HashGet<RComponentCache>(Key, id);
        }

    }
}