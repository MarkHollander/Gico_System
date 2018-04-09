using Dapper;
using Gico.Config;
using Gico.MarketingDataObject.Interfaces.Banner;
using Gico.ReadSystemModels.Banner;
using Gico.SystemDomains.Banner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Gico.Common;
using System.Linq;

namespace Gico.MarketingDataObject.Implements.Banner
{
    public class BannerItemRepository : SqlBaseDao, IBannerItemRepository
    {
        public async Task<RBannerItem> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RBannerItem>(ProcName.BannerItem_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RBannerItem[]> GetById(string[] ids)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                return (await connection.QueryAsync<RBannerItem>(ProcName.BannerItem_GetByIds, parameters, commandType: CommandType.StoredProcedure)).ToArray();
            });
        }

        public async Task<RBannerItem[]> GetByBannerId(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BannerId", id, DbType.String);
                return (await connection.QueryAsync<RBannerItem>(ProcName.BannerItem_GetByBannerId, parameters, commandType: CommandType.StoredProcedure)).ToArray();
            });
        }

        public async Task<RBannerItem[]> Search(string id, string bannerItemName, string bannerId, EnumDefine.CommonStatusEnum status, bool isDefault, DateTimeRange startDate, DateTimeRange endDate, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", string.IsNullOrEmpty(id) ? string.Empty : id, DbType.String);
                parameters.Add("@BannerItemName", string.IsNullOrEmpty(bannerItemName) ? string.Empty : bannerItemName, DbType.String);
                parameters.Add("@BannerId", string.IsNullOrEmpty(bannerId) ? string.Empty : bannerId, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@IsDefault", isDefault, DbType.Boolean);
                parameters.Add("@StartDateUtcFrom", startDate.FromDate, DbType.DateTime);
                parameters.Add("@StartDateUtcTo", startDate.ToDate, DbType.DateTime);
                parameters.Add("@EndDateUtcFrom", endDate.FromDate, DbType.DateTime);
                parameters.Add("@EndDateUtcTo", endDate.ToDate, DbType.DateTime);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int32);
                parameters.Add("@DeletedStatus", EnumDefine.CommonStatusEnum.Deleted.AsEnumToInt(), DbType.Int32);
                var data = await connection.QueryAsync<RBannerItem>(ProcName.BannerItem_Search, parameters, commandType: CommandType.StoredProcedure);

                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        public async Task<bool> Add(BannerItem bannerItem)
        {
            return await WithConnection(async (connection) =>
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", bannerItem.Id, DbType.String);
                    parameters.Add("@BannerItemName", bannerItem.BannerItemName, DbType.String);
                    parameters.Add("@BannerId", bannerItem.BannerId, DbType.String);
                    parameters.Add("@TargetUrl", bannerItem.TargetUrl, DbType.String);
                    parameters.Add("@ImageUrl", bannerItem.ImageUrl, DbType.String);
                    parameters.Add("@Status", bannerItem.Status.AsEnumToInt(), DbType.Int16);
                    parameters.Add("@StartDateUtc", bannerItem.StartDateUtc, DbType.DateTime);
                    parameters.Add("@EndDateUtc", bannerItem.EndDateUtc, DbType.DateTime);
                    parameters.Add("@IsDefault", bannerItem.IsDefault, DbType.Boolean);
                    parameters.Add("@BackgroundRGB", bannerItem.BackgroundRGB, DbType.String);
                    parameters.Add("@CreatedDateUtc", bannerItem.CreatedDateUtc, DbType.DateTime);
                    parameters.Add("@CreatedUid", bannerItem.CreatedUid, DbType.String);
                    var rowCount = 0;
                    rowCount = await connection.ExecuteAsync(ProcName.BannerItem_Add, parameters, commandType: CommandType.StoredProcedure);
                    if (rowCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            });
        }

        public async Task<bool> Change(BannerItem bannerItem)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", bannerItem.Id, DbType.String);
                parameters.Add("@BannerItemName", bannerItem.BannerItemName, DbType.String);
                parameters.Add("@BannerId", bannerItem.BannerId, DbType.String);
                parameters.Add("@TargetUrl", bannerItem.TargetUrl, DbType.String);
                parameters.Add("@ImageUrl", bannerItem.ImageUrl, DbType.String);
                parameters.Add("@Status", bannerItem.Status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@StartDateUtc", bannerItem.StartDateUtc, DbType.DateTime);
                parameters.Add("@EndDateUtc", bannerItem.EndDateUtc, DbType.DateTime);
                parameters.Add("@IsDefault", bannerItem.IsDefault, DbType.Boolean);
                parameters.Add("@BackgroundRGB", bannerItem.BackgroundRGB, DbType.String);
                parameters.Add("@UpdatedDateUtc", bannerItem.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", bannerItem.UpdatedUid, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.BannerItem_Change, parameters, commandType: CommandType.StoredProcedure);
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

        public async Task ChangeStatus(string id, EnumDefine.CommonStatusEnum status, string userId, DateTime updatedDate)
        {
            await WithConnection(async (connection) =>
           {
               DynamicParameters parameters = new DynamicParameters();
               parameters.Add("@Id", id, DbType.String);
               parameters.Add("@Status", status.AsEnumToInt(), DbType.Int16);
               parameters.Add("@UpdatedDateUtc", updatedDate, DbType.DateTime);
               parameters.Add("@UpdatedUid", userId, DbType.String);
               return await connection.ExecuteAsync(ProcName.BannerItem_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);

           });
        }
    }
}
