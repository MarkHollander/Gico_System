using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderDomains.Giftcodes;
using Gico.ReadCartModels;
using Gico.ReadOrderModels.Giftcodes;

namespace Gico.OrderDataObject.Implements
{
    public class GiftcodeCampaignRepository : SqlBaseDao, IGiftcodeCampaignRepository
    {
        public async Task<RGiftCodeCampaign> Get(string connectionString, string id)
        {
            var data = await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                var result = await connection.QueryFirstOrDefaultAsync<RGiftCodeCampaign>(ProcName.GiftCodeCampaign_GetById, parameters, commandType: CommandType.StoredProcedure);
                return result;
            });
            return data;
        }

        public async Task<RGiftCodeCampaign[]> Gets(string connectionString, string name, DateTime? beginDate, DateTime? endDate, EnumDefine.GiftCodeCampaignStatus status, RefSqlPaging sqlPaging)
        {
            var data = await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@BeginDate", beginDate, DbType.DateTime);
                parameters.Add("@EndDate", endDate, DbType.DateTime);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int64);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var result = await connection.QueryAsync<RGiftCodeCampaign>(ProcName.GiftCodeCampaign_Gets, parameters, commandType: CommandType.StoredProcedure);
                return result.ToArray();
            });
            if (data.Length > 0)
            {
                sqlPaging.TotalRow = data[0].TotalRow;
            }
            return data;
        }
        public async Task<int> Add(string connectionString, GiftCodeCampaign campaign)
        {
            var data = await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", campaign.Id, DbType.String);
                parameters.Add("@ParentId", string.Empty, DbType.String);
                parameters.Add("@NAME", campaign.Name, DbType.String);
                parameters.Add("@Notes", campaign.Notes, DbType.String);
                parameters.Add("@BeginDate", campaign.BeginDate, DbType.String);
                parameters.Add("@EndDate", campaign.EndDate, DbType.String);
                parameters.Add("@GiftCodeCalendar", campaign.GiftCodeCalendarsSerialize, DbType.String);
                parameters.Add("@MESSAGE", campaign.Message, DbType.String);
                parameters.Add("@STATUS", campaign.Status, DbType.Int64);
                parameters.Add("@AllowPaymentOnCheckout", campaign.AllowPaymentOnCheckout, DbType.Boolean);
                parameters.Add("@Conditions", campaign.GiftCodeConditionsSerialize, DbType.String);
                parameters.Add("@CreatedDateUtc", campaign.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", campaign.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", campaign.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", campaign.UpdatedUid, DbType.String);
                parameters.Add("@ShardId", campaign.ShardId, DbType.Int32);
                parameters.Add("@VERSION", campaign.Version, DbType.Int32);
                var result = await connection.ExecuteAsync(ProcName.GiftCodeCampaign_Add, parameters, commandType: CommandType.StoredProcedure);
                return result;
            });
            return data;
        }
        public async Task<int> Change(string connectionString, GiftCodeCampaign campaign)
        {
            var data = await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", campaign.Id, DbType.String);
                parameters.Add("@NAME", campaign.Name, DbType.String);
                parameters.Add("@Notes", campaign.Notes, DbType.String);
                parameters.Add("@BeginDate", campaign.BeginDate, DbType.String);
                parameters.Add("@EndDate", campaign.EndDate, DbType.String);
                parameters.Add("@GiftCodeCalendar", campaign.GiftCodeCalendarsSerialize, DbType.String);
                parameters.Add("@MESSAGE", campaign.Message, DbType.String);
                parameters.Add("@STATUS", campaign.Status, DbType.Int64);
                parameters.Add("@AllowPaymentOnCheckout", campaign.AllowPaymentOnCheckout, DbType.Boolean);
                parameters.Add("@Conditions", campaign.GiftCodeConditionsSerialize, DbType.String);
                parameters.Add("@UpdatedDateUtc", campaign.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", campaign.UpdatedUid, DbType.String);
                parameters.Add("@VERSION", campaign.Version, DbType.Int32);
                var result = await connection.ExecuteAsync(ProcName.GiftCodeCampaign_Change, parameters, commandType: CommandType.StoredProcedure);
                return result;
            });
            return data;
        }
        public async Task<int> ChangeStatus(string connectionString, GiftCodeCampaign campaign, bool isApproved)
        {
            var data = await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", campaign.Id, DbType.String);
                parameters.Add("@STATUS", campaign.Name, DbType.String);
                parameters.Add("@UpdatedDateUtc", campaign.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", campaign.UpdatedUid, DbType.String);
                parameters.Add("@VERSION", campaign.Version, DbType.Int32);
                parameters.Add("@IsApproved", isApproved, DbType.Boolean);
                var result = await connection.ExecuteAsync(ProcName.GiftCodeCampaign_Change, parameters, commandType: CommandType.StoredProcedure);
                return result;
            });
            return data;
        }
    }
}