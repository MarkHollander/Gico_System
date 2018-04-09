using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class ShardingConfigAddOrChangeRequest: BaseRequest
    {
        public ShardingConfigModel ShardingConfig { get; set; }
    }
}