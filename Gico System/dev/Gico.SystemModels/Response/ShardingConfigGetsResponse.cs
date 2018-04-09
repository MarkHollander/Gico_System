using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class ShardingConfigGetsResponse : BaseResponse
    {
        public ShardingConfigModel[] ShardingConfigs { get; set; }
    }
}