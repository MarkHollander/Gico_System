using Gico.Config;
using Gico.DataObject;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class ShardingConfigGetResponse : BaseResponse
    {
        public ShardingConfigGetResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.ShardTypeEnum));
            ShardGroups = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.ShardGroupEnum));
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.ShardStatusEnum), false);
            ShardingConfig = new ShardingConfigModel();
        }
        public ShardingConfigGetResponse(ShardingConfigModel shardingConfig)
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.ShardTypeEnum));
            ShardGroups = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.ShardGroupEnum));
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.ShardStatusEnum), (int)shardingConfig.Status, false);
            ShardingConfig = shardingConfig;
        }
        public ShardingConfigModel ShardingConfig { get; private set; }
        public KeyValueTypeIntModel[] Types { get; set; }
        public KeyValueTypeIntModel[] ShardGroups { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; set; }
    }
}