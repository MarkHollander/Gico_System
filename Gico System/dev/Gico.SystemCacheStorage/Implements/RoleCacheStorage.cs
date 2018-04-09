using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemCacheStorage.Implements
{
    public class RoleCacheStorage : CacheStorage, IRoleCacheStorage
    {
        public RoleCacheStorage(IRedisStorage redisStorage) : base(redisStorage)
        {
        }

        private const string ActionDefineStorageKey = "ActionDefine";

        public async Task<bool> ActionDefineAdd(RActionDefine actionDefine)
        {
            return await RedisStorage.HashSet(ActionDefineStorageKey, actionDefine.Id, actionDefine);
        }
        public async Task<RActionDefine> ActionDefineGet(string id)
        {
            return await RedisStorage.HashGet<RActionDefine>(ActionDefineStorageKey, id);
        }
        public async Task<RActionDefine[]> ActionDefineGet(string[] ids)
        {
            return await RedisStorage.HashGet<RActionDefine>(ActionDefineStorageKey, ids);
        }

        public async Task<bool> CheckExists(string id)
        {
            return await RedisStorage.HashExists(ActionDefineStorageKey, id);
        }
    }
}