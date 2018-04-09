using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class ShardingConfigGetsRequest : BaseRequest
    {
        public int ShardGroup { get; set; }
    }
}