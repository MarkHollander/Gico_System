using System.Threading.Tasks;
using Gico.Config;
using Gico.DataObject;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.ShardingDataObject.Interfaces
{
    public interface IShardingConfigRepository
    {
        #region WRITE

        Task Add(ShardingConfig shardingConfig);
        Task Change(ShardingConfig shardingConfig);

        #endregion

        #region Read
        Task<RShardingConfig[]> Get(EnumDefine.ShardGroupEnum group, EnumDefine.ShardStatusEnum status);
        Task<RShardingConfig[]> Get(EnumDefine.ShardGroupEnum group);
        Task<RShardingConfig> Get(int id);

        #endregion


    }
}