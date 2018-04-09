using System;
using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.OrderCacheStorage.Interfaces;
using Gico.ReadCartModels;

namespace Gico.OrderCacheStorage.Implements
{
    public class CartCacheStorage : CacheStorage, ICartCacheStorage
    {
        public CartCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }

        #region Key Define

        public static string CreateCartKey(string clientId)
        {
            return $"CreateCartKey:{clientId}";
        }

        public static string CartKey = "CreateCartKey";
        #endregion

        public async Task<bool> CreatingCart(string clientId)
        {
            string key = CreateCartKey(clientId);
            return await RedisStorage.LockTake(key, clientId, TimeSpan.FromMinutes(1));
        }

        public async Task<bool> CreatedCart(string clientId)
        {
            string key = CreateCartKey(clientId);
            return await RedisStorage.LockRelease(key, clientId);
        }

        public async Task<RCart> Get(string clientId)
        {
            string key = CartKey;
            return await RedisStorage.HashGet<RCart>(key, clientId);
        }

        public async Task<bool> Add(RCart cart)
        {
            string key = CartKey;
            return await RedisStorage.HashSet(key, cart.ClientId, cart);
        }
    }
}