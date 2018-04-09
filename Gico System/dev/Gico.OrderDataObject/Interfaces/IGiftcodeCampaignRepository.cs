using System;
using Gico.ReadOrderModels.Giftcodes;
using System.Threading.Tasks;
using Gico.Config;
using Gico.OrderDomains.Giftcodes;

namespace Gico.OrderDataObject.Interfaces
{
    public interface IGiftcodeCampaignRepository
    {
        Task<RGiftCodeCampaign> Get(string connectionString, string id);
        Task<RGiftCodeCampaign[]> Gets(string connectionString, string name, DateTime? beginDate, DateTime? endDate, EnumDefine.GiftCodeCampaignStatus status, RefSqlPaging sqlPaging);
        Task<int> Add(string connectionString, GiftCodeCampaign campaign);
        Task<int> Change(string connectionString, GiftCodeCampaign campaign);
        Task<int> ChangeStatus(string connectionString, GiftCodeCampaign campaign, bool isApproved);
    }
}