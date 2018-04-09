using System;
using System.Linq;
using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.Common;
using Gico.Config;
using Gico.DataObject;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Interfaces;
using Microsoft.Extensions.Logging;
using Gico.ShardingConfigService.Interfaces;
using Gico.SystemAppService.Mapping;
using Gico.SystemDomains;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Implements
{
    public class ShardingAppService : IShardingAppService
    {
        private readonly ILogger<ShardingAppService> _logger;
        private readonly IShardingService _shardingService;

        public ShardingAppService(IShardingService shardingService, ILogger<ShardingAppService> logger)
        {
            _shardingService = shardingService;
            _logger = logger;
        }

        public async Task<ShardingConfigGetResponse> ShardingConfigGet(ShardingConfigGetRequest request)
        {
            ShardingConfigGetResponse response = new ShardingConfigGetResponse();
            try
            {
                if (request.Id > 0)
                {
                    RShardingConfig shardingConfig = await _shardingService.Get(request.Id);
                    if (shardingConfig == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.ShardingConfigNotFound);
                        return response;
                    }
                    response = new ShardingConfigGetResponse(shardingConfig.ToModel());
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

        public async Task<ShardingConfigAddOrChangeResponse> ShardingConfigAddOrChange(ShardingConfigAddOrChangeRequest request)
        {
            ShardingConfigAddOrChangeResponse response = new ShardingConfigAddOrChangeResponse();
            try
            {
                ShardingConfig shardingConfig = request.ShardingConfig.ToObject();
                if (shardingConfig.Id > 0)
                {
                    shardingConfig.UpdatedDate = Extensions.GetCurrentDateUtc();
                    await _shardingService.Change(shardingConfig);
                }
                else
                {
                    //var loginInfo =await _userService.GetLoginInfo(request.SessionId);
                    //shardingConfig.CreatedUid = loginInfo.Id;
                    shardingConfig.CreatedDate = Extensions.GetCurrentDateUtc();
                    await _shardingService.Add(shardingConfig);
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

        public async Task<ShardingConfigGetsResponse> ShardingConfigGets(ShardingConfigGetsRequest request)
        {
            ShardingConfigGetsResponse response = new ShardingConfigGetsResponse();
            try
            {
                RShardingConfig[] shardingConfigs = await _shardingService.Get((EnumDefine.ShardGroupEnum)request.ShardGroup);
                response.ShardingConfigs = shardingConfigs.Select(p => p.ToModel()).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

    }
}