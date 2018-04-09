using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemCacheStorage.Implements
{
    public class LanguageCacheStorage : CacheStorage, ILanguageCacheStorage
    {
        public LanguageCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }

        private const string StorageKey = "LanguageCacheStorage";

        public async Task<RLanguage[]> Get()
        {
            return await RedisStorage.HashGetAll<RLanguage>(StorageKey);
        }

        public async Task<RLanguage> Get(string id)
        {
            return await RedisStorage.HashGet<RLanguage>(StorageKey, id);
        }

        public async Task<bool> AddOrChange(RLanguage currency)
        {
            return await RedisStorage.HashSet(StorageKey, currency.Id, currency);
        }

        public async Task<bool> Remove(string id)
        {
            return await RedisStorage.HashDelete(StorageKey, id);
        }


    }
}