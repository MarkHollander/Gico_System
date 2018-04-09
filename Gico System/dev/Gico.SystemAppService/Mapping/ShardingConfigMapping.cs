using Gico.DataObject;
using Gico.ReadSystemModels;
using Gico.SystemDomains;
using Gico.SystemModels.Models;

namespace Gico.SystemAppService.Mapping
{
    public static class ShardingConfigMapping
    {
        public static ShardingConfigModel ToModel(this RShardingConfig request)
        {
            if (request == null) return null;
            return new ShardingConfigModel()
            {
                Id = request.Id,
                CreatedDate = request.CreatedDate,
                CreatedUid = request.CreatedUid,
                HostName = request.HostName,
                ShardGroup = request.ShardGroup,
                DatabaseName = request.DatabaseName,
                Uid = request.Uid,
                Type = request.Type,
                Config = request.Config,
                Pwd = request.Pwd,
                Status = request.Status,
                UpdatedDate = request.UpdatedDate,
                UpdatedUid = request.UpdatedUid,
                ShardGroupName = request.ShardGroupName,
                StatusName = request.StatusName,
                TypeName = request.TypeName
            };
        }


        public static ShardingConfig ToObject(this ShardingConfigModel request)
        {
            if (request == null) return null;
            return new ShardingConfig()
            {
                Id = request.Id,
                CreatedDate = request.CreatedDate,
                CreatedUid = request.CreatedUid,
                HostName = request.HostName,
                ShardGroup = request.ShardGroup,
                DatabaseName = request.DatabaseName,
                Uid = request.Uid,
                Type = request.Type,
                Config = request.Config,
                Pwd = request.Pwd,
                Status = request.Status,
                UpdatedDate = request.UpdatedDate,
                UpdatedUid = request.UpdatedUid,
                
            };
        }
    }

}