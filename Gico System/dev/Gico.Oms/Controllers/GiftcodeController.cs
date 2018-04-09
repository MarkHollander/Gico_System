using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gico.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Gico.Oms.Models;
using Gico.Oms.Validations;
using Microsoft.Extensions.Logging;
using Gico.OmsAppService.Interfaces;
using Gico.OmsModels.Request;

namespace Gico.Oms.Controllers
{
    public class GiftcodeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IGiftcodeAppService _giftcodeAppService;
        public GiftcodeController(ILogger<GiftcodeController> logger, IGiftcodeAppService giftcodeAppService)
        {
            _logger = logger;
            _giftcodeAppService = giftcodeAppService;
        }

        public async Task<IActionResult> Campaigns([FromBody] GiftCodeCampaignGetsRequest request)
        {
            try
            {
                var response = await _giftcodeAppService.GiftCodeCampaignGet(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
        public async Task<IActionResult> Campaign([FromBody] GiftCodeCampaignGetRequest request)
        {
            try
            {
                var response = await _giftcodeAppService.GiftCodeCampaignGet(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
        public async Task<IActionResult> CampaignAdd([FromBody] GiftCodeCampaignAddOrChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                var results = GiftCodeCampaignAddRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _giftcodeAppService.GiftCodeCampaignAddOrChange(request);
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
        public async Task<IActionResult> CampaignChange([FromBody] GiftCodeCampaignAddOrChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                var results = GiftCodeCampaignChangeRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _giftcodeAppService.GiftCodeCampaignAddOrChange(request);
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);

            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
        public async Task<IActionResult> CampaignChangeStatus([FromBody] GiftCodeCampaignChangeStatusRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                var results = GiftCodeCampaignChangeStatusRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _giftcodeAppService.GiftCodeCampaignChangeStatus(request);
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);

            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
    }
}
