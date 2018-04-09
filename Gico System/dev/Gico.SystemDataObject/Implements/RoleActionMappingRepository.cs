using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class RoleActionMappingRepository : SqlBaseDao, IRoleActionMappingRepository
    {

        public async Task<RRoleActionMapping[]> GetByCustomerId(string customerId)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId, DbType.String);
                var data = await connection.QueryAsync<RRoleActionMapping>(ProcName.Role_Action_Mapping_GetByCustomerId, parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
        public async Task<RRoleActionMapping[]> GetByRoleId(string roleId)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoleId", roleId, DbType.String);
                var data = await connection.QueryAsync<RRoleActionMapping>(ProcName.Role_Action_Mapping_GetByRoleId, parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task Change(RoleActionMapping[] roleActionMappingsAdd, string roleId, string[] actionIdsRemove)
        {
            await WithConnection(async (connection, transaction) =>
            {
                int rowDelete = 0;
                if (actionIdsRemove?.Length > 0)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@RoleId", roleId, DbType.String);
                    parameters.Add("@ActionIds", string.Join(",", actionIdsRemove), DbType.String);
                    rowDelete = await connection.ExecuteAsync(ProcName.Role_Action_Mapping_RemoveByRoleIdAndActionIds, parameters,transaction, commandType: CommandType.StoredProcedure);
                }
                if (roleActionMappingsAdd != null && roleActionMappingsAdd.Length > 0)
                {
                    DataTable dataTable = RoleActionMapping.CreateDataTable();
                    foreach (var roleActionMapping in roleActionMappingsAdd)
                    {
                        roleActionMapping.AddToDataTable(dataTable);
                    }
                    await BulkCopy(dataTable, connection, transaction);
                }
                return rowDelete;
            });

        }
    }
}