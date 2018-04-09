using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class ManufacturerRepository : SqlBaseDao, IManufacturerRepository
    {
        public async Task<RManufacturer[]> GetAll()
        {
            var datas = await WithConnection(async (connection) =>
            {
                return await connection.QueryAsync<RManufacturer>(ProcName.Manufacturer_GetAll, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }

        public async Task<RManufacturer[]> Search(string name, EnumDefine.StatusEnum status, RefSqlPaging paging)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@OFFSET", paging.OffSet, DbType.String);
                parameters.Add("@FETCH", paging.PageSize, DbType.String);
                return await connection.QueryAsync<RManufacturer>(ProcName.Manufacturer_SearchByName, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }

        public async Task<RManufacturer> GetById(string id)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RManufacturer>(ProcName.Manufacturer_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas;
        }

        public async Task<RManufacturer[]> GetById(string[] ids)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                return await connection.QueryAsync<RManufacturer>(ProcName.Manufacturer_GetByIds, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }

        public async Task Add(Manufacturer manufacturer)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nameManu", manufacturer.Name, DbType.String);
                parameters.Add("@Des", manufacturer.Description, DbType.String);
                parameters.Add("@Logo", manufacturer.Logo, DbType.String);
                parameters.Add("@CreatedDateUtc", manufacturer.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", manufacturer.UpdatedDateUtc, DbType.DateTime);

                return await connection.ExecuteAsync(ProcName.Manufacturer_Add, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<bool> Change(Manufacturer manufacturer)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", manufacturer.Id, DbType.String);
                parameters.Add("@Name", manufacturer.Name, DbType.String);
                parameters.Add("@Description", manufacturer.Description, DbType.String);
                parameters.Add("@Logo", manufacturer.Logo, DbType.String);
                parameters.Add("@UpdatedDateUtc", manufacturer.UpdatedDateUtc, DbType.DateTime);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Manufacturer_Change, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;
            });
        }

    }
}
