using Gico.Caching.Redis;
using Gico.SystemCacheStorage.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCacheStorage.Implements.Product
{
    public class ProductCacheStorage: CacheStorage, IProductCacheStorage
    {
        public ProductCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }
    }
}
