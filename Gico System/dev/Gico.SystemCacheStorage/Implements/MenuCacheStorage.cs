using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemCacheStorage.Implements
{
    public class MenuCacheStorage : CacheStorage, IMenuCacheStorage
    {
        public MenuCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }

        private string StorageKey(string languageId)
        {
            return $"MenuCacheStorage_languageId_{languageId}";
        }

        public async Task<RMenu[]> Get(string languageId)
        {
            string key = StorageKey(languageId);
            return await RedisStorage.HashGetAll<RMenu>(key);
        }

        public async Task<RMenu> Get(string languageId, string id)
        {
            string key = StorageKey(languageId);
            return await RedisStorage.HashGet<RMenu>(key, id);
        }

        public async Task<bool> AddOrChange(RMenu menu)
        {
            string key = StorageKey(menu.LanguageId);
            return await RedisStorage.HashSet(key, menu.Id, menu);
        }

        public async Task<bool> Remove(string languageId, string id)
        {
            string key = StorageKey(languageId);
            return await RedisStorage.HashDelete(key, id);
        }
    }
}
