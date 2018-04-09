using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.Models.Response;
using Gico.OmsAppService.Interfaces;
using Gico.OmsAppService.Mapping;
using Gico.OmsModels.Models;
using Gico.OmsModels.Request;
using Gico.OmsModels.Response;
using Gico.OrderService.Interfaces;
using Gico.ShardingConfigService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gico.OmsAppService.Implements
{
    public class GiftcodeAppService : IGiftcodeAppService
    {
        private readonly ILogger<GiftcodeAppService> _logger;
        private readonly IGiftcodeService _giftcodeService;
        private readonly IShardingService _shardingService;
        private readonly ICurrentContext _currentContext;
        public GiftcodeAppService(ILogger<GiftcodeAppService> logger, IGiftcodeService giftcodeService, IShardingService shardingService, ICurrentContext currentContext)
        {
            _logger = logger;
            _giftcodeService = giftcodeService;
            _shardingService = shardingService;
            _currentContext = currentContext;
        }

        public async Task<GiftCodeCampaignGetsResponse> GiftCodeCampaignGet(GiftCodeCampaignGetsRequest request)
        {
            GiftCodeCampaignGetsResponse response = new GiftCodeCampaignGetsResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _giftcodeService.GiftCodeGetsFromDb(string.Empty, request.CampaignName, request.BeginDateValue, request.EndDateValue, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.Campaigns = data.Select(p => p.ToModel()).ToArray();
                response.PageIndex = request.PageIndex;
                response.PageSize = request.PageSize;
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<GiftCodeCampaignGetResponse> GiftCodeCampaignGet(GiftCodeCampaignGetRequest request)
        {
            GiftCodeCampaignGetResponse response = new GiftCodeCampaignGetResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    response.Campaign = new GiftCodeCampaignViewModel();
                }
                else
                {
                    var shard = await _shardingService.Get(request.ShardId);
                    var campaign = await _giftcodeService.GiftCodeGetFromDb(shard.ConnectionString, request.Id);
                    if (campaign == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.Order_GiftCodeCampaignNotFound);
                        return response;
                    }
                    response.Campaign = campaign.ToModel();
                }
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> GiftCodeCampaignAddOrChange(GiftCodeCampaignAddOrChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                CommandResult result;
                var currentUser = await _currentContext.GetCurrentCustomer();
                if (string.IsNullOrEmpty(request.Id))
                {
                    var command = request.ToCommand(currentUser.Id);
                    result = await _giftcodeService.SendCommand(command);
                }
                else
                {
                    var command = request.ToCommand(currentUser.Id, request.Version, request.ShardId);
                    result = await _giftcodeService.SendCommand(command);
                }
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> GiftCodeCampaignChangeStatus(GiftCodeCampaignChangeStatusRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var currentUser = await _currentContext.GetCurrentCustomer();
                var command = request.ToCommand(currentUser.Id, request.Version, request.ShardId);
                var result = await _giftcodeService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
        
        public async Task<GiftCodeGroupGetsResponse> Get(GiftCodeGroupGetsRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GiftCodeGroupGetResponse> Get(GiftCodeGroupGetRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}