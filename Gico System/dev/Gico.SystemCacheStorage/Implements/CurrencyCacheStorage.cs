using System;
using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemEvents.Cache;
using Gico.ReadSystemModels;

namespace Gico.SystemCacheStorage.Implements
{
    public class CurrencyCacheStorage : ICurrencyCacheStorage
    {
        private readonly IRedisStorage _redisStorage;

        private const string StorageKey = "CurrencyCacheStorage";
        public CurrencyCacheStorage(IRedisStorage redisStorage)
        {
            _redisStorage = redisStorage;
        }
        public async Task<RCurrency[]> Get()
        {
            return await _redisStorage.HashGetAll<RCurrency>(StorageKey);
        }
        public async Task<RCurrency> Get(string id)
        {
            return await _redisStorage.HashGet<RCurrency>(StorageKey, id);
        }

        public async Task AddOrChange(RCurrency currency)
        {
            await _redisStorage.HashSet(StorageKey, currency.Id, currency);
        }

        public async Task Remove(string id)
        {
            await _redisStorage.HashDelete(StorageKey, id);
        }


    }
}
