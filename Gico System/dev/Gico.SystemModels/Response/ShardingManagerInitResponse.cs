using Gico.Config;
using Gico.DataObject;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class ShardingManagerInitResponse
    {
        public ShardingManagerInitResponse()
        {
            ShardGroups = KeyValueTypeStringModel.FromEnum(typeof(EnumDefine.ShardGroupEnum));
        }
        public KeyValueTypeStringModel[] ShardGroups { get; set; }

    }
}