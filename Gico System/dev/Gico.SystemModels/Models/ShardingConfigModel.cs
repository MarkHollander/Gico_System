using System;
using Gico.Config;

namespace Gico.SystemModels.Models
{
    public class ShardingConfigModel
    {
        public int Id { get; set; }
        public EnumDefine.ShardStatusEnum Status { get; set; }
        public string HostName { get; set; }
        public string DatabaseName { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUid { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUid { get; set; }
        public EnumDefine.ShardTypeEnum Type { get; set; }
        public EnumDefine.ShardGroupEnum ShardGroup { get; set; }
        public string Config { get; set; }
        public string ConnectionString => $"Server={HostName};Database={DatabaseName};uid={Uid};pwd={Pwd};";
        public string StatusName { get; set; }
        public string TypeName { get; set; }
        public string ShardGroupName { get; set; }
    }
}
