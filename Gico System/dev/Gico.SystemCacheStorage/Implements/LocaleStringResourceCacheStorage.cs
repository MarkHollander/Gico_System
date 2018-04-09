using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemCacheStorage.Implements
{
    public class LocaleStringResourceCacheStorage : CacheStorage, ILocaleStringResourceCacheStorage
    {
        public LocaleStringResourceCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }

        private string StorageKey(string languageId)
        {
            return $"LocaleStringResourceCacheStorage_languageId_{languageId}";
        }

        public async Task<RLocaleStringResource[]> Get(string[] resourceNames, string languageId)
        {
            string key = StorageKey(languageId);
            return await RedisStorage.HashGet<RLocaleStringResource>(key, resourceNames);
        }
        
        public async Task<bool> AddOrChange(RLocaleStringResource localeStringResource)
        {
            string key = StorageKey(localeStringResource.LanguageId);
            return await RedisStorage.HashSet(key, localeStringResource.ResourceName, localeStringResource);
        }

        public async Task<bool> Remove(string id, string languageId)
        {
            string key = StorageKey(languageId);
            return await RedisStorage.HashDelete(key, id);
        }


    }
}