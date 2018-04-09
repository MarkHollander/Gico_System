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
    public class ProductAttributeRepository : SqlBaseDao, IProductAttributeRepository
    {
        public async Task<RProductAttribute[]> Search(string attributeId, string attributeName, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AttributeId", attributeId, DbType.String);
                parameters.Add("@AttributeName", attributeName, DbType.String);
                parameters.Add("@OffSet", paging.OffSet, DbType.Int32);
                parameters.Add("@PageSize", paging.PageSize, DbType.Int32);
                var data = await connection.QueryAsync<RProductAttribute>(ProcName.ProductAttribute_Search, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Any())
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        public async Task<RProductAttribute> Get(string attributeId)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AttributeId", attributeId, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RProductAttribute>(ProcName.ProductAttribute_Get, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RProductAttribute[]> Get(string[] attributeIds)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", attributeIds), DbType.String);
                return (await connection.QueryAsync<RProductAttribute>(ProcName.ProductAttribute_GetByIds, parameters, commandType: CommandType.StoredProcedure)).ToArray();
            });
        }


        #region CRUD

        public async Task Add(ProductAttribute productAttribute)
        {
            await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", productAttribute.Name, DbType.String);
                parameters.Add("@Status", productAttribute.Status, DbType.Int16);
                parameters.Add("@CreatedOnUtc", productAttribute.CreatedOnUtc, DbType.DateTime);
                parameters.Add("@CreatedUserId", productAttribute.CreatedUserId, DbType.String);

                var data = await connection.ExecuteAsync(ProcName.ProductAttribute_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<bool> Update(ProductAttribute productAttribute)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", productAttribute.Id, DbType.String);
                parameters.Add("@Name", productAttribute.Name, DbType.String);
                parameters.Add("@Status", productAttribute.Status, DbType.Int16);
                parameters.Add("@UpdatedOnUtc", productAttribute.UpdatedOnUtc, DbType.DateTime);
                parameters.Add("@UpdatedUserId", productAttribute.UpdatedUserId, DbType.String);
                var rowCount = await connection.ExecuteAsync(ProcName.ProductAttribute_Update, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;
            });
        }

        #endregion
    }

    public class ProductAttributeValueRepository : SqlBaseDao, IProductAttributeValueRepository
    {
        public async Task<RProductAttributeValue[]> Search(string attributeId, string attributeValueId, string value, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AttributeId", attributeId, DbType.String);
                parameters.Add("@AttributeValueId", attributeValueId, DbType.String);
                parameters.Add("@Value", value, DbType.String);
                parameters.Add("@OffSet", paging.OffSet, DbType.Int32);
                parameters.Add("@PageSize", paging.PageSize, DbType.Int32);
                var data = await connection.QueryAsync<RProductAttributeValue>(ProcName.ProductAttributeValue_Search, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Any())
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        public async Task<RProductAttributeValue> Get(string attributeValueId)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AttributeValueId", attributeValueId, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RProductAttributeValue>(ProcName.ProductAttributeValue_Get, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RProductAttributeValue[]> Get(string[] attributeValueIds)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", attributeValueIds), DbType.String);
                return (await connection.QueryAsync<RProductAttributeValue>(ProcName.ProductAttributeValue_GetByIds, parameters, commandType: CommandType.StoredProcedure)).ToArray();
            });
        }


        #region CRUD

        public async Task Add(ProductAttributeValue productAttributeValue)
        {
            await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AttributeId", productAttributeValue.AttributeId, DbType.String);
                parameters.Add("@Value", productAttributeValue.Value, DbType.String);
                parameters.Add("@UnitId", productAttributeValue.UnitId, DbType.Int32);
                parameters.Add("@AttributeValueStatus", productAttributeValue.AttributeValueStatus, DbType.Int16);
                parameters.Add("@CreatedOnUtc", productAttributeValue.CreatedOnUtc, DbType.DateTime);
                parameters.Add("@CreatedUserId", productAttributeValue.CreatedUserId, DbType.String);
                parameters.Add("@DisplayOrder", productAttributeValue.DisplayOrder, DbType.Int32);

                var data = await connection.ExecuteAsync(ProcName.ProductAttributeValue_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<bool> Update(ProductAttributeValue productAttributeValue)
        {
            return await WithConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", productAttributeValue.Id, DbType.String);
                parameters.Add("@AttributeId", productAttributeValue.AttributeId, DbType.String);
                parameters.Add("@Value", productAttributeValue.Value, DbType.String);
                parameters.Add("@UnitId", productAttributeValue.UnitId, DbType.Int32);
                parameters.Add("@AttributeValueStatus", productAttributeValue.AttributeValueStatus, DbType.Int16);
                parameters.Add("@UpdatedOnUtc", productAttributeValue.UpdatedOnUtc, DbType.DateTime);
                parameters.Add("@UpdatedUserId", productAttributeValue.UpdatedUserId, DbType.String);
                parameters.Add("@DisplayOrder", productAttributeValue.DisplayOrder, DbType.Int32);

                var rowCount = await connection.ExecuteAsync(ProcName.ProductAttributeValue_Update, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;
            });
        }

        #endregion
    }
}