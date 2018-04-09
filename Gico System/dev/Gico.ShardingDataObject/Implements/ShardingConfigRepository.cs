using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.DataObject;
using Gico.ReadSystemModels;
using Gico.ShardingDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.ShardingDataObject.Implements
{
    public class ShardingConfigRepository : SqlBaseDao, IShardingConfigRepository
    {
        #region Write

        public async Task Add(ShardingConfig shardingConfig)
        {
            const string spName = ProcName.ShardingConfig_Add;
            await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@STATUS", shardingConfig.Status, DbType.Int32);
                parameters.Add("@HostName", shardingConfig.HostName, DbType.String);
                parameters.Add("@DatabaseName", shardingConfig.DatabaseName, DbType.String);
                parameters.Add("@Uid", shardingConfig.Uid, DbType.String);
                parameters.Add("@Pwd", shardingConfig.Pwd, DbType.String);
                parameters.Add("@CreatedDate", shardingConfig.CreatedDate, DbType.DateTime);
                parameters.Add("@CreatedUid", shardingConfig.CreatedUid ?? string.Empty, DbType.String);
                parameters.Add("@UpdatedDate", shardingConfig.CreatedDate, DbType.DateTime);
                parameters.Add("@UpdatedUid", shardingConfig.CreatedUid ?? string.Empty, DbType.String);
                parameters.Add("@TYPE", shardingConfig.Type, DbType.Int32);
                parameters.Add("@ShardGroup", shardingConfig.ShardGroup, DbType.Int32);
                parameters.Add("@Config", shardingConfig.Config ?? string.Empty, DbType.String);
                return await p.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task Change(ShardingConfig shardingConfig)
        {
            const string spName = ProcName.ShardingConfig_Change;
            await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", shardingConfig.Id, DbType.Int32);
                parameters.Add("@STATUS", shardingConfig.Status, DbType.Int32);
                parameters.Add("@HostName", shardingConfig.HostName, DbType.String);
                parameters.Add("@DatabaseName", shardingConfig.DatabaseName, DbType.String);
                parameters.Add("@Uid", shardingConfig.Uid, DbType.String);
                parameters.Add("@Pwd", shardingConfig.Pwd, DbType.String);
                parameters.Add("@UpdatedDate", shardingConfig.CreatedDate, DbType.DateTime);
                parameters.Add("@UpdatedUid", shardingConfig.CreatedUid ?? string.Empty, DbType.String);
                parameters.Add("@TYPE", shardingConfig.Type, DbType.Int32);
                parameters.Add("@ShardGroup", shardingConfig.ShardGroup, DbType.Int32);
                parameters.Add("@Config", shardingConfig.Config, DbType.String);
                return await p.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        #endregion

        #region read

        public async Task<RShardingConfig[]> Get(EnumDefine.ShardGroupEnum @group, EnumDefine.ShardStatusEnum status)
        {
            return await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Group", (int)@group, DbType.Int32);
                parameters.Add("@Status", (int)status, DbType.Int32);
                var datas = await p.QueryAsync<RShardingConfig>(ProcName.ShardingConfig_GetByGroupAndStatus, parameters, commandType: CommandType.StoredProcedure);
                return datas.ToArray();
            });
        }

        public async Task<RShardingConfig[]> Get(EnumDefine.ShardGroupEnum group)
        {
            const string spName = ProcName.ShardingConfig_GetByGroup;
            var data = await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Group", (int)group, DbType.Int32);
                return await p.QueryAsync<RShardingConfig>(spName, parameters, commandType: CommandType.StoredProcedure);
            });
            return data.ToArray();
        }

        public async Task<RShardingConfig> Get(int id)
        {
            const string spName = ProcName.ShardingConfig_Get;
            var data = await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int32);
                return await p.QueryFirstAsync<RShardingConfig>(spName, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        #endregion

    }
}