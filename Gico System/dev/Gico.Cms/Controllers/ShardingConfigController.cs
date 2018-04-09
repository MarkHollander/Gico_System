using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.Cms.Validations;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ShardingConfigController : Controller
    {
        private readonly ILogger _logger;
        private readonly IShardingAppService _shardingAppService;
        public ShardingConfigController(ILogger<ShardingConfigController> logger, IShardingAppService shardingAppService)
        {
            _logger = logger;
            _shardingAppService = shardingAppService;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Init()
        {
            try
            {
                ShardingManagerInitResponse response = await Task.FromResult(new ShardingManagerInitResponse());
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] ShardingConfigGetRequest request)
        {
            try
            {
                var response = await _shardingAppService.ShardingConfigGet(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddOrChange([FromBody] ShardingConfigAddOrChangeRequest request)
        {
            try
            {
                ShardingConfigAddOrChangeResponse response = new ShardingConfigAddOrChangeResponse();
                var results = ShardingConfigAddRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    results = ShardingConfigAddModelValidator.ValidateModel(request.ShardingConfig);
                    if (results.IsValid)
                    {
                        response = await _shardingAppService.ShardingConfigAddOrChange(request);
                    }
                    else
                    {
                        response.SetFail(results.Errors.Select(p => p.ToString()));
                    }
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Gets([FromBody] ShardingConfigGetsRequest request)
        {
            try
            {
                var response = await _shardingAppService.ShardingConfigGets(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }
    }
}