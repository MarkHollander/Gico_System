using System;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RShardingConfig
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string HostName { get; set; }
        [ProtoMember(3)]
        public string DatabaseName { get; set; }
        [ProtoMember(4)]
        public string Uid { get; set; }
        [ProtoMember(5)]
        public string Pwd { get; set; }
        [ProtoMember(6)]
        public DateTime CreatedDate { get; set; }
        [ProtoMember(7)]
        public string CreatedUid { get; set; }
        [ProtoMember(8)]
        public DateTime UpdatedDate { get; set; }
        [ProtoMember(9)]
        public string UpdatedUid { get; set; }
        [ProtoMember(10)]
        public EnumDefine.ShardTypeEnum Type { get; set; }
        [ProtoMember(11)]
        public EnumDefine.ShardGroupEnum ShardGroup { get; set; }
        [ProtoMember(12)]
        public EnumDefine.ShardStatusEnum Status { get; set; }
        [ProtoMember(13)]
        public string Config { get; set; }
        [ProtoMember(14)]
        public string StatusName { get; set; }
        [ProtoMember(15)]
        public string TypeName { get; set; }
        [ProtoMember(16)]
        public string ShardGroupName { get; set; }
        public string ConnectionString => $"Server={HostName};Database={DatabaseName};uid={Uid};pwd={Pwd};Trusted_Connection=False;MultipleActiveResultSets=True;Max Pool Size=1000";
    }
}