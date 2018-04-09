using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels.Warehouse;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class WarehouseRepository : SqlBaseDao, IWarehouseRepository
    {
        public async Task<RWarehouse> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RWarehouse>(ProcName.Warehouse_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RWarehouse[]> GetById(string[] ids)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                var data = await connection.QueryAsync<RWarehouse>(ProcName.Warehouse_GetByIds, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();

                return dataReturn;
            });
        }

        public async Task<RWarehouse[]> Search(string code, string email, string phone, string name, EnumDefine.WarehouseStatusEnum status, EnumDefine.WarehouseTypeEnum type, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@VendorId", code, DbType.String);
                parameters.Add("@Code", code, DbType.String);
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@PhoneNumber", phone, DbType.String);
                parameters.Add("@WarehouseName", name, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@Type", type.AsEnumToInt(), DbType.Int16);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int16);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int16);
                var data = await connection.QueryAsync<RWarehouse>(ProcName.Warehouse_Search, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        public async Task<RWarehouse[]> Search(string venderId, string keyword, EnumDefine.WarehouseStatusEnum status, EnumDefine.WarehouseTypeEnum type, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@VendorId", venderId, DbType.String);
                parameters.Add("@Keyword", keyword, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@Type", type.AsEnumToInt(), DbType.Int16);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int16);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int16);
                var data = await connection.QueryAsync<RWarehouse>(ProcName.Warehouse_SearchByKeyword, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

    }
}
