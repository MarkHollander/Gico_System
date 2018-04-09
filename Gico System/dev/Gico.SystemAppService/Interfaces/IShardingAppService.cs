using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface IShardingAppService
    {
        Task<ShardingConfigGetResponse> ShardingConfigGet(ShardingConfigGetRequest request);
        Task<ShardingConfigAddOrChangeResponse> ShardingConfigAddOrChange(ShardingConfigAddOrChangeRequest request);
        Task<ShardingConfigGetsResponse> ShardingConfigGets(ShardingConfigGetsRequest request);
    }
}