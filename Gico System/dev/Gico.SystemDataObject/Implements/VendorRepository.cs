using System;
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
    public class VendorRepository : SqlBaseDao, IVendorRepository
    {

        #region read 
        public async Task<RVendor> GetById(string id)
        {
            return await WithConnection(async (connection) =>
             {
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@Id", id, DbType.String);
                 return await connection.QueryFirstOrDefaultAsync<RVendor>(ProcName.Vendor_GetById, parameters, commandType: CommandType.StoredProcedure);
             });
        }

        public async Task<RVendor[]> GetFromDb(string[] ids)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                return (await connection.QueryAsync<RVendor>(ProcName.Vendor_GetByIds, parameters, commandType: CommandType.StoredProcedure)).ToArray();
            });
        }


        public async Task<RVendor[]> Search(string code, string email, string phone, string name, EnumDefine.VendorStatusEnum status, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Code", code, DbType.String);
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@Phone", phone, DbType.String);
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.String);
                parameters.Add("@OFFSET", paging.OffSet, DbType.String);
                parameters.Add("@FETCH", paging.PageSize, DbType.String);
                var data = await connection.QueryAsync<RVendor>(ProcName.Vendor_Search, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }
        public async Task<RVendor[]> Search(string keyword, EnumDefine.VendorStatusEnum status, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", keyword, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.String);
                parameters.Add("@OFFSET", paging.OffSet, DbType.String);
                parameters.Add("@FETCH", paging.PageSize, DbType.String);
                var data = await connection.QueryAsync<RVendor>(ProcName.Vendor_SearchByKeyword, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        #endregion

        #region write
        public async Task Add(Vendor vendor)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", vendor.Id, DbType.String);
                parameters.Add("@Email", vendor.Email, DbType.String);
                parameters.Add("@Name", vendor.Name, DbType.String);
                parameters.Add("@Description", vendor.Description, DbType.String);
                parameters.Add("@Fax", vendor.Fax, DbType.String);
                parameters.Add("@CompanyName", vendor.CompanyName, DbType.String);
                parameters.Add("@Website", vendor.Website, DbType.String);
                parameters.Add("@Logo", vendor.Logo, DbType.String);
                parameters.Add("@Code", vendor.Code, DbType.String);
                parameters.Add("@Phone", vendor.Phone, DbType.String);

                parameters.Add("@TYPE", vendor.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@STATUS", vendor.Status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@CreatedDateUtc", vendor.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", vendor.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", vendor.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", vendor.UpdatedUid, DbType.String);
                parameters.Add("@VERSION", vendor.Version, DbType.Int32);
                return await connection.ExecuteAsync(ProcName.Vendor_Add, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<bool> Change(Vendor vendor, string code)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", vendor.Id, DbType.String);
                if (!string.IsNullOrEmpty(code))
                {
                    parameters.Add("@Code", code, DbType.String);
                }
                parameters.Add("@Email", vendor.Email, DbType.String);
                parameters.Add("@Name", vendor.Name, DbType.String);
                parameters.Add("@Description", vendor.Description, DbType.String);
                parameters.Add("@Fax", vendor.Fax, DbType.String);
                parameters.Add("@CompanyName", vendor.CompanyName, DbType.String);
                parameters.Add("@Website", vendor.Website, DbType.String);
                parameters.Add("@Logo", vendor.Logo, DbType.String);
                parameters.Add("@Phone", vendor.Phone, DbType.String);

                parameters.Add("@TYPE", vendor.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@STATUS", vendor.Status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@CreatedDateUtc", vendor.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", vendor.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", vendor.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", vendor.UpdatedUid, DbType.String);
                parameters.Add("@VERSION", vendor.Version, DbType.Int32);
                var rowCount = 0;
                if (!string.IsNullOrEmpty(code))
                {
                    rowCount = await connection.ExecuteAsync(ProcName.Vendor_ChangeAndChangeCode, parameters, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    rowCount = await connection.ExecuteAsync(ProcName.Vendor_Change, parameters, commandType: CommandType.StoredProcedure);
                }
                return rowCount == 1;
            });
        }
        #endregion
    }
}