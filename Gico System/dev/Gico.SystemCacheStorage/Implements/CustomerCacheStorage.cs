using System;
using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemCacheStorage.Implements
{
    public class CustomerCacheStorage : CacheStorage, ICustomerCacheStorage
    {
        public CustomerCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }

        public async Task SetLoginInfo(string key, RCustomer customer)
        {
            await RedisStorage.StringSet(key, customer,
                TimeSpan.FromMinutes(ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt()));
        }
        public async Task<RCustomer> GetLoginInfo(string key)
        {
            return await RedisStorage.StringGet<RCustomer>(key);
        }

        public async Task<bool> CheckLoginInfoExist(string key)
        {
            return await RedisStorage.KeyExist(key);
        }
    }
}