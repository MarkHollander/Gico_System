using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class DepartmentRepository : SqlBaseDao, IDepartmentRepository
    {
        public async Task<RDepartment[]> Search(string name, EnumDefine.DepartmentStatusEnum status, RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int64);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var data = (await connection.QueryAsync<RDepartment>(ProcName.Department_Search, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    sqlPaging.TotalRow = data[0].TotalRow;
                }
                return data.ToArray();
            });
        }

        public async Task Add(Department department)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", department.Id, DbType.String);
                parameters.Add("@NAME", department.Name, DbType.String);
                parameters.Add("@STATUS", department.Status, DbType.Int64);
                var data = await connection.ExecuteAsync(ProcName.Department_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task Change(Department department)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", department.Id, DbType.String);
                parameters.Add("@NAME", department.Name, DbType.String);
                parameters.Add("@STATUS", department.Status, DbType.Int64);
                var data = await connection.ExecuteAsync(ProcName.Department_Change, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RDepartment> Get(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RDepartment>(ProcName.Department_Get, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RDepartment[]> Get(string[] ids)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                var data = await connection.QueryAsync<RDepartment>(ProcName.Department_GetByIds, parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
    }
}