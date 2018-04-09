using Gico.CQRS.Service.Interfaces;
using Gico.DataObject;
using System;
using System.Data;
using System.Threading.Tasks;
using Gico.Config;
using Gico.Domains;

namespace Gico.ShardingDataObject
{
    public class SqlBaseDao : BaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.ShardingMasterConnectionString.GetConfig();
        public class ProcName
        {
            #region ShardingConfig

            public const string ShardingConfig_Add = "ShardingConfig_Add";
            public const string ShardingConfig_Change = "ShardingConfig_Change";
            public const string ShardingConfig_GetByGroup = "ShardingConfig_GetByGroup";
            public const string ShardingConfig_Get = "ShardingConfig_Get";
            public const string ShardingConfig_GetByGroupAndStatus = "ShardingConfig_GetByGroupAndStatus";

            #endregion


        }
    }
}
