using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ExceptionDefine;
using Gico.OrderCommands;
using Gico.OrderCommands.Giftcodes;
using Gico.OrderDomains;
using Gico.OrderDomains.Giftcodes;
using Gico.OrderService.Interfaces;
using Gico.ReadCartModels;
using Gico.ReadSystemModels;
using Gico.ShardingConfigService.Interfaces;

namespace Gico.OrderCommandsHandler
{
    public class GiftcodeCommandHandler : ICommandHandler<GiftCodeCampaignAddCommand, ICommandResult>,
        ICommandHandler<GiftCodeCampaignChangeCommand, ICommandResult>,
        ICommandHandler<GiftCodeCampaignChangeStatusCommand, ICommandResult>
    {
        private EnumDefine.ShardGroupEnum ShardGroup = EnumDefine.ShardGroupEnum.Order;
        private readonly IShardingService _shardingService;
        private readonly IGiftcodeService _giftcodeService;
        private readonly IEventSender _eventSender;

        public GiftcodeCommandHandler(IEventSender eventSender, IShardingService shardingService, IGiftcodeService giftcodeService)
        {
            _eventSender = eventSender;
            _shardingService = shardingService;
            _giftcodeService = giftcodeService;
        }

        public async Task<ICommandResult> Handle(GiftCodeCampaignAddCommand mesage)
        {
            try
            {
                var shard = await _shardingService.GetCurrentWriteShardByRoundRobin(ShardGroup);
                GiftCodeCampaign campaign = new GiftCodeCampaign();
                campaign.Add(mesage, shard.Id);
                int rowCount = await _giftcodeService.Add(shard.ConnectionString, campaign);
                if (rowCount <= 0)
                {
                    throw new MessageException(ResourceKey.GiftCodeCampaign_AddFail);
                }
                await _eventSender.Notify();
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = string.Empty,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(GiftCodeCampaignChangeCommand mesage)
        {
            try
            {
                var shard = await _shardingService.Get(mesage.ShardId);
                if (shard == null)
                {
                    throw new MessageException(ResourceKey.ShardingConfig_NotFound);
                }
                GiftCodeCampaign campaign = new GiftCodeCampaign();
                campaign.Change(mesage);
                int rowCount = await _giftcodeService.Change(shard.ConnectionString, campaign);
                if (rowCount <= 0)
                {
                    throw new MessageException(ResourceKey.GiftCodeCampaignCart_NotFound);
                }
                await _eventSender.Notify();
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = string.Empty,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(GiftCodeCampaignChangeStatusCommand mesage)
        {
            try
            {
                var shard = await _shardingService.Get(mesage.ShardId);
                if (shard == null)
                {
                    throw new MessageException(ResourceKey.ShardingConfig_NotFound);
                }
                GiftCodeCampaign campaign = new GiftCodeCampaign();
                bool isApproved = campaign.ChangeStatus(mesage);
                int rowCount = await _giftcodeService.ChangeStatus(shard.ConnectionString, campaign, isApproved);
                if (rowCount <= 0)
                {
                    throw new MessageException(ResourceKey.GiftCodeCampaignCart_NotFound);
                }
                await _eventSender.Notify();
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = string.Empty,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }
    }
}