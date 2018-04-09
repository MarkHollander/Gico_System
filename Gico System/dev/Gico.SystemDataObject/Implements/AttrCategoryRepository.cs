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
    public class AttrCategoryRepository : SqlBaseDao, IAttrCategoryRepository
    {
        public async Task Add(AttrCategory attrCategory)
        {
            await WithConnection(async (connection) =>
            {
                
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AttributeId", attrCategory.AttributeId, DbType.Int16);
                parameters.Add("@CategoryId", attrCategory.CategoryId, DbType.String);
                parameters.Add("@IsFilter", attrCategory.IsFilter, DbType.Boolean);
                parameters.Add("@FilterSpan", attrCategory.FilterSpan, DbType.String);
                parameters.Add("@BaseUnitId", attrCategory.BaseUnitId, DbType.Int16);
                parameters.Add("@AttributeType", attrCategory.AttributeType, DbType.Int16);
                parameters.Add("@DisplayOrder", attrCategory.DisplayOrder, DbType.Int16);
                parameters.Add("@IsRequired", attrCategory.IsRequired, DbType.Boolean);
              
                return await connection.ExecuteAsync(ProcName.AttributeCategoryMapping_Add, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RAttrCategory> Get(int attributeId, string categoryId)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AttributeId", attributeId, DbType.Int16);
                parameters.Add("@CategoryId", categoryId, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RAttrCategory>(ProcName.AttributeCategoryMapping_Get, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        public async Task<bool> Change(AttrCategory attrCategory)
        {
            return await WithConnection(async (connection) =>
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AttributeId", attrCategory.AttributeId, DbType.Int16);
                parameters.Add("@CategoryId", attrCategory.CategoryId, DbType.String);
                parameters.Add("@IsFilter", attrCategory.IsFilter, DbType.Boolean);
                parameters.Add("@FilterSpan", attrCategory.FilterSpan, DbType.String);
                parameters.Add("@BaseUnitId", attrCategory.BaseUnitId, DbType.Int16);
                parameters.Add("@AttributeType", attrCategory.AttributeType, DbType.Int16);
                parameters.Add("@DisplayOrder", attrCategory.DisplayOrder, DbType.Int16);
                parameters.Add("@IsRequired", attrCategory.IsRequired, DbType.Boolean);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.AttributeCategoryMapping_Change, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;
                
            });
        }

        public async Task Remove(AttrCategory attrCategory)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AttributeId", attrCategory.AttributeId, DbType.Int16);
                parameters.Add("@CategoryId", attrCategory.CategoryId, DbType.String);
                return await connection.ExecuteAsync(ProcName.AttributeCategoryMapping_Delete, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RProductAttribute[]> GetsProductAttr(string categoryId)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", categoryId, DbType.String);
                return await connection.QueryAsync<RProductAttribute>(ProcName.AttributeCategoryMapping_GetsProductAttr, parameters, commandType: CommandType.StoredProcedure);
            });
            return data.ToArray();
        }
    }
}
