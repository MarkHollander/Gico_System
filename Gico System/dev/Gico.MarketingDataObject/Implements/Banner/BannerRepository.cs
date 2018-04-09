using Dapper;
using Gico.Config;
using Gico.MarketingDataObject.Interfaces.Banner;
using Gico.ReadSystemModels.Banner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Gico.Common;
using System.Linq;

namespace Gico.MarketingDataObject.Implements.Banner
{
    public class BannerRepository : SqlBaseDao, IBannerRepository
    {
        public async Task<RBanner> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RBanner>(ProcName.Banner_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
        }
        public async Task<RBanner[]> GetById(string[] ids)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                return (await connection.QueryAsync<RBanner>(ProcName.Banner_GetByIds, parameters, commandType: CommandType.StoredProcedure)).ToArray();
            });
        }

        public async Task<RBanner[]> Search(string id, string bannerName, EnumDefine.CommonStatusEnum status, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", string.IsNullOrEmpty(id) ? string.Empty : id, DbType.String);
                parameters.Add("@BannerName", string.IsNullOrEmpty(bannerName) ? string.Empty : bannerName, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int32);
                parameters.Add("@DeletedStatus", EnumDefine.CommonStatusEnum.Deleted.AsEnumToInt(), DbType.Int32);
                var data = await connection.QueryAsync<RBanner>(ProcName.Banner_Search, parameters, commandType: CommandType.StoredProcedure);

                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        public async Task<RBanner[]> GetByMenuId(string menuId)
        {
            return await WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuId", menuId, DbType.String);
                var data = await connection.QueryAsync<RBanner>(ProcName.Banner_GetByMenuId, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                return dataReturn;
            });
        }

        public async Task<bool> Add(Gico.SystemDomains.Banner.Banner banner)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", banner.Id, DbType.String);
                parameters.Add("@BannerName", banner.BannerName, DbType.String);
                parameters.Add("@BackgroundRGB", banner.BackgroundRGB, DbType.String);
                parameters.Add("@Status", banner.Status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@CreatedDateUtc", banner.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", banner.CreatedUid, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Banner_Add, parameters, commandType: CommandType.StoredProcedure);
                if (rowCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public async Task<bool> Change(Gico.SystemDomains.Banner.Banner banner)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", banner.Id, DbType.String);
                parameters.Add("@BannerName", banner.BannerName, DbType.String);
                parameters.Add("@BackgroundRGB", banner.BackgroundRGB, DbType.String);
                parameters.Add("@Status", banner.Status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@UpdatedDateUtc", banner.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", banner.UpdatedUid, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Banner_Change, parameters, commandType: CommandType.StoredProcedure);
                if (rowCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public async Task ChangeBannerStatus(string id, EnumDefine.CommonStatusEnum bannerStatus, string updatedUid, DateTime updatedDate)
        {
            await WithConnection(async (connection) =>
           {
               DynamicParameters parameters = new DynamicParameters();
               parameters.Add("@Id", id, DbType.String);
               parameters.Add("@Status", bannerStatus.AsEnumToInt(), DbType.Int16);
               parameters.Add("@UpdatedDateUtc", updatedDate, DbType.DateTime);
               parameters.Add("@UpdatedUid", updatedUid, DbType.String);
               return await connection.ExecuteAsync(ProcName.Banner_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
           });
        }
    }
}
