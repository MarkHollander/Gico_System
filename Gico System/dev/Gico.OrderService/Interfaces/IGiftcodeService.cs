using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.OrderCommands.Giftcodes;
using Gico.OrderDomains.Giftcodes;
using Gico.ReadOrderModels.Giftcodes;

namespace Gico.OrderService.Interfaces
{
    public interface IGiftcodeService
    {
        #region READ From DB
        Task<RGiftCodeCampaign> GiftCodeGetFromDb(string connectionString, string id);
        Task<RGiftCodeCampaign[]> GiftCodeGetsFromDb(string connectionString, string name, DateTime? beginDate, DateTime? endDate, EnumDefine.GiftCodeCampaignStatus status, RefSqlPaging sqlPaging);
        #endregion

        #region Write To Db
        Task<int> Add(string connectionString, GiftCodeCampaign campaign);
        Task<int> Change(string connectionString, GiftCodeCampaign campaign);
        Task<int> ChangeStatus(string connectionString, GiftCodeCampaign campaign, bool isApproved);
        #endregion

        #region Command

        Task<CommandResult> SendCommand(GiftCodeCampaignAddCommand command);
        Task<CommandResult> SendCommand(GiftCodeCampaignChangeCommand command);
        Task<CommandResult> SendCommand(GiftCodeCampaignChangeStatusCommand command);

        #endregion

        #region Add To Cache

        #endregion

        #region Get From Cache

        #endregion

        #region Common

        #endregion
    }
}