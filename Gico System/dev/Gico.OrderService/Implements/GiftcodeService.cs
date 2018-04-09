using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.OrderCommands.Giftcodes;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderDomains.Giftcodes;
using Gico.OrderService.Interfaces;
using Gico.ReadOrderModels.Giftcodes;

namespace Gico.OrderService.Implements
{
    public class GiftcodeService : IGiftcodeService
    {
        private readonly ICommandSender _commandService;
        private readonly IGiftcodeCampaignRepository _giftcodeCampaignRepository;

        public GiftcodeService(IGiftcodeCampaignRepository giftcodeCampaignRepository, ICommandSender commandService)
        {
            _giftcodeCampaignRepository = giftcodeCampaignRepository;
            _commandService = commandService;
        }
        #region READ From DB
        public async Task<RGiftCodeCampaign> GiftCodeGetFromDb(string connectionString, string id)
        {
            return await _giftcodeCampaignRepository.Get(connectionString, id);
        }

        public async Task<RGiftCodeCampaign[]> GiftCodeGetsFromDb(string connectionString, string name, DateTime? beginDate, DateTime? endDate, EnumDefine.GiftCodeCampaignStatus status,
            RefSqlPaging sqlPaging)
        {
            return await _giftcodeCampaignRepository.Gets(connectionString, name, beginDate, endDate, status,
                sqlPaging);
        }

        #endregion

        #region Write To Db
        public async Task<int> Add(string connectionString, GiftCodeCampaign campaign)
        {
            return await _giftcodeCampaignRepository.Add(connectionString, campaign);
        }

        public async Task<int> Change(string connectionString, GiftCodeCampaign campaign)
        {
            return await _giftcodeCampaignRepository.Change(connectionString, campaign);
        }

        public async Task<int> ChangeStatus(string connectionString, GiftCodeCampaign campaign, bool isApproved)
        {
            return await _giftcodeCampaignRepository.ChangeStatus(connectionString, campaign, isApproved);
        }
        #endregion

        #region Command

        public async Task<CommandResult> SendCommand(GiftCodeCampaignAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(GiftCodeCampaignChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(GiftCodeCampaignChangeStatusCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        #region Add To Cache

        #endregion

        #region Get From Cache

        #endregion

        #region Common

        #endregion
    }
}