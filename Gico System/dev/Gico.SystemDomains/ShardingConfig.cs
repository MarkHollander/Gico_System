using System;
using Gico.Config;
using Gico.Domains;

namespace Gico.SystemDomains
{
    public class ShardingConfig:BaseDomain
    {
        public int Id { get; set; }
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
        public EnumDefine.ShardStatusEnum Status { get; set; }
        public string Config { get; set; }
        public string ConnectionString => $"Server={HostName};Database={DatabaseName};uid={Uid};pwd={Pwd};Trusted_Connection=False;MultipleActiveResultSets=True;Max Pool Size=1000";

        public string StatusName
        {
            get
            {
                string name = string.Empty;
                var values = Enum.GetValues(typeof(EnumDefine.ShardStatusEnum));
                foreach (var value in values)
                {
                    if (CheckSatus((EnumDefine.ShardStatusEnum)value))
                    {
                        name += ((EnumDefine.ShardStatusEnum)value).GetDisplayName() + ", ";
                    }
                }
                return name.Trim().TrimEnd(',');
            }
        }

        public string TypeName => Type.GetDisplayName();

        public string ShardGroupName => ShardGroup.GetDisplayName();

        public bool CheckSatus(EnumDefine.ShardStatusEnum statusEnum)
        {
            return Status.HasFlag(statusEnum);
        }

        public void AddStatus(EnumDefine.ShardStatusEnum statusEnum)
        {
            Status |= statusEnum;
        }
        public void RemoveStatus(EnumDefine.ShardStatusEnum statusEnum)
        {
            Status &= ~statusEnum;
        }

    }
}