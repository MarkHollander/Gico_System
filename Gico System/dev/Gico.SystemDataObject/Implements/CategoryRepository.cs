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
    public class CategoryRepository : SqlBaseDao, ICategoryRepository
    {
        public async Task<RCategory[]> Get(string languageId)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LanguageId", languageId, DbType.String);
                return await connection.QueryAsync<RCategory>(ProcName.Category_GetByLanguageId, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }
        public async Task<RCategory> Get(string languageId, string id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RCategory>(ProcName.Category_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        public async Task Add(Category category)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", category.Id, DbType.String);
                parameters.Add("@ParentId", category.ParentId, DbType.String);
                parameters.Add("@Name", category.Name, DbType.String);
                parameters.Add("@Description", category.Description, DbType.String);
                parameters.Add("@CreatedDateUtc", category.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", category.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", category.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", category.CreatedUid, DbType.String);
                parameters.Add("@Code", category.Code, DbType.String);
                parameters.Add("@LanguageId", category.LanguageId, DbType.String);
                parameters.Add("@Status", category.Status, DbType.Int64);
                parameters.Add("@DisplayOrder", category.DisplayOrder, DbType.Int64);
                parameters.Add("@Version", category.Version, DbType.Int64);
                parameters.Add("@LanguageId", category.LanguageId, DbType.String);

                return await connection.ExecuteAsync(ProcName.Category_Add, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<bool> Change(Category category)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", category.Id, DbType.String);
                parameters.Add("@ParentId", category.ParentId, DbType.String);
                parameters.Add("@Name", category.Name, DbType.String);
                parameters.Add("@Description", category.Description, DbType.String);
                parameters.Add("@CreatedDateUtc", category.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", category.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", category.CreatedUid, DbType.String);
                parameters.Add("@Code", category.Code, DbType.String);
                parameters.Add("@UpdatedUid", category.CreatedUid, DbType.String);
                parameters.Add("@LanguageId", category.LanguageId, DbType.String);
                parameters.Add("@Status", category.Status, DbType.Int64);
                parameters.Add("@DisplayOrder", category.DisplayOrder, DbType.Int64);
                parameters.Add("@Version", category.Version, DbType.Int64);
                parameters.Add("@LanguageId", category.LanguageId, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Category_Change, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;

            });
        }

        public async Task<RCategoryAttr[]> GetListAttr(string id, RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var data = (await connection.QueryAsync<RCategoryAttr>(ProcName.Category_GetListAttr, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    sqlPaging.TotalRow = data[0].TotalRow;
                }
                return data;
            });
        }

        public async Task<RManufacturer[]> GetListManufacturer(string id, RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var data = (await connection.QueryAsync<RManufacturer>(ProcName.Category_GetListManufacturer, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    sqlPaging.TotalRow = data[0].TotalRow;
                }
                return data;
            });
        }
    }
}
