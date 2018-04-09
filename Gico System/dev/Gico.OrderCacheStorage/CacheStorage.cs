using Gico.Caching.Redis;
using System;

namespace Gico.OrderCacheStorage
{
    public class CacheStorage
    {
        protected readonly IRedisStorage RedisStorage;

        protected CacheStorage(IRedisStorage redisStorage)
        {
            RedisStorage = redisStorage;
        }
    }
}
