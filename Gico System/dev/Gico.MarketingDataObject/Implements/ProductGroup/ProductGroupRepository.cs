using Gico.MarketingDataObject.Interfaces.ProductGroup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.ReadSystemModels.ProductGroup;

namespace Gico.MarketingDataObject.Implements.ProductGroup
{
    public class ProductGroupRepository : SqlBaseDao, IProductGroupRepository
    {
        public async Task<RProductGroup[]> Search(string name, EnumDefine.CommonStatusEnum status, RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@DeleteStatus", EnumDefine.CommonStatusEnum.Deleted.AsEnumToInt(), DbType.Int32);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var data = await connection.QueryAsync<RProductGroup>(ProcName.ProductGroup_Search, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    sqlPaging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        public async Task<RProductGroup> Get(string id)
        {
            return await WithConnection(async (connection) =>
             {
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@Id", id, DbType.String);
                 return await connection.QueryFirstOrDefaultAsync<RProductGroup>(ProcName.ProductGroup_Get, parameters, commandType: CommandType.StoredProcedure);
             });
        }

        public async Task Add(SystemDomains.ProductGroup.ProductGroup productGroup)
        {
            await WithConnection(async (connection) =>
           {
               DynamicParameters parameters = new DynamicParameters();
               parameters.Add("@Id", productGroup.Id, DbType.String);
               parameters.Add("@Name", productGroup.Name, DbType.String);
               parameters.Add("@Status", productGroup.Status.AsEnumToInt(), DbType.Int32);
               parameters.Add("@DESCRIPTION", productGroup.Description, DbType.String);
               parameters.Add("@Conditions", string.Empty, DbType.String);
               parameters.Add("@UpdatedDateUtc", productGroup.CreatedDateUtc, DbType.DateTime);
               parameters.Add("@CreatedDateUtc", productGroup.CreatedDateUtc, DbType.DateTime);
               parameters.Add("@CreatedUid", productGroup.CreatedUid, DbType.String);
               parameters.Add("@UpdatedUid", productGroup.CreatedUid, DbType.String);
               return await connection.ExecuteAsync(ProcName.ProductGroup_Add, parameters, commandType: CommandType.StoredProcedure);
           });
        }
        public async Task Change(SystemDomains.ProductGroup.ProductGroup productGroup)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", productGroup.Id, DbType.String);
                parameters.Add("@Name", productGroup.Name, DbType.String);
                parameters.Add("@Status", productGroup.Status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@DESCRIPTION", productGroup.Description, DbType.String);
                parameters.Add("@UpdatedDateUtc", productGroup.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", productGroup.UpdatedUid, DbType.String);
                return await connection.ExecuteAsync(ProcName.ProductGroup_Change, parameters, commandType: CommandType.StoredProcedure);
            });
        }
        public async Task ChangeConditions(string id, string conditions, string updatedUid, DateTime updatedDate)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@Conditions", conditions, DbType.String);
                parameters.Add("@UpdatedDateUtc", updatedDate, DbType.DateTime);
                parameters.Add("@UpdatedUid", updatedUid, DbType.String);
                return await connection.ExecuteAsync(ProcName.ProductGroup_ChangeConditions, parameters, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
