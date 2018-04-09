using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class ActionDefineRepository : SqlBaseDao, IActionDefineRepository
    {
        public async Task<RActionDefine> Get(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RActionDefine>(ProcName.ActionDefine_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
        }
        public async Task<RActionDefine[]> Get(string[] ids)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                var data = await connection.QueryAsync<RActionDefine>(ProcName.ActionDefine_GetByIds, parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
        public async Task<RActionDefine[]> Get(string group, string name, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Group", group, DbType.String);
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int32);
                var data = (await connection.QueryAsync<RActionDefine>(ProcName.ActionDefine_Search, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    paging.TotalRow = data[0].TotalRow;
                }
                return data;
            });
        }

        public async Task Add(ActionDefine actionDefine)
        {
            await WithConnection(async (connection) =>
           {
               DynamicParameters parameters = new DynamicParameters();
               parameters.Add("@Id", actionDefine.Id, DbType.String);
               parameters.Add("@NAME", actionDefine.Name, DbType.String);
               parameters.Add("@Group", actionDefine.Group, DbType.String);
               var data = await connection.ExecuteAsync(ProcName.ActionDefine_Add, parameters, commandType: CommandType.StoredProcedure);
               return data;
           });
        }
    }
}