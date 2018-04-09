using System.Linq;
using System.Threading.Tasks;
using Gico.Common;
using Gico.Config;
using Gico.DataObject;
using Gico.ReadSystemModels;
using Gico.ShardingConfigService.Interfaces;
using Gico.ShardingDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.ShardingConfigService.Implements
{
    public class ShardingService : IShardingService
    {
        private readonly IShardingConfigRepository _shardingConfigDao;

        public ShardingService(IShardingConfigRepository shardingConfigDao)
        {
            _shardingConfigDao = shardingConfigDao;
        }

        public async Task Add(ShardingConfig shardingConfig)
        {
            await _shardingConfigDao.Add(shardingConfig);
        }

        public async Task Change(ShardingConfig shardingConfig)
        {
            await _shardingConfigDao.Change(shardingConfig);
        }

        public async Task<RShardingConfig[]> Get(EnumDefine.ShardGroupEnum group)
        {
            return await _shardingConfigDao.Get(group);
        }

        public async Task<RShardingConfig> Get(int id)
        {
            return await _shardingConfigDao.Get(id);
        }

        public static int CurrentShardIndex = 0;
        public static readonly object GetNextShardIndexLock = new object();

        private int GetNextShardIndex(int totalShard)
        {
            lock (GetNextShardIndexLock)
            {
                int nextShardIndex = CurrentShardIndex++ % totalShard;
                CurrentShardIndex = nextShardIndex;
                return nextShardIndex;
            }
        }
        public async Task<RShardingConfig> GetCurrentWriteShardByRoundRobin(EnumDefine.ShardGroupEnum shardGroup)
        {
            RShardingConfig[] shardingConfigs = await _shardingConfigDao.Get(shardGroup, EnumDefine.ShardStatusEnum.IsWrite);
            int nextShardIndex = GetNextShardIndex(shardingConfigs.Length);
            return shardingConfigs[nextShardIndex];
        }
        public async Task<RShardingConfig> GetCurrentWriteShardByYear(EnumDefine.ShardGroupEnum shardGroup)
        {
            RShardingConfig[] shardingConfigs = await _shardingConfigDao.Get(shardGroup, EnumDefine.ShardStatusEnum.IsWrite);
            var currentShard = shardingConfigs.FirstOrDefault(p => Serialize.JsonDeserializeObject<YearTypeConfig>(p.Config).Year == Extensions.GetCurrentDateUtc().Year);
            return currentShard;
        }
        public async Task<RShardingConfig> GetShardById(EnumDefine.ShardGroupEnum shardGroup, int shardId)
        {
            RShardingConfig[] shardingConfigs = await _shardingConfigDao.Get(shardGroup, EnumDefine.ShardStatusEnum.IsWrite);
            return shardingConfigs.FirstOrDefault(p => p.Id == shardId);
        }
    }
}
