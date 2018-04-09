using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;

namespace Gico.SystemService.Interfaces
{
    public interface ICommonService
    {
        Task<long> GetNextId(Type objType);
        Task<string> GetNextCode(Type objType);
        Task<string> GetNextId();
        Task SetFlagRegisterSuccessAsync(string id);
        Task<bool> GetFlagRegisterSuccessAsync(string id);
        Task<RShardingConfig[]> ShardingConfigGetFromCache(EnumDefine.ShardGroupEnum shardGroup);
    }
}