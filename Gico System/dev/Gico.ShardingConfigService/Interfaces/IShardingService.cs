using System.Threading.Tasks;
using Gico.Config;
using Gico.DataObject;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.ShardingConfigService.Interfaces
{
    public interface IShardingService
    {
        Task Add(ShardingConfig shardingConfig);
        Task Change(ShardingConfig shardingConfig);
        Task<RShardingConfig[]> Get(EnumDefine.ShardGroupEnum group);
        Task<RShardingConfig> Get(int id);

        Task<RShardingConfig> GetCurrentWriteShardByRoundRobin(EnumDefine.ShardGroupEnum shardGroup);
        Task<RShardingConfig> GetCurrentWriteShardByYear(EnumDefine.ShardGroupEnum shardGroup);
        Task<RShardingConfig> GetShardById(EnumDefine.ShardGroupEnum shardGroup, int shardId);
    }
}