using Gico.Caching.Redis;

namespace Gico.SystemCacheStorage
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