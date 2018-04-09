using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Gico.SystemAppService.Interfaces.Banner;
using Gico.SystemModels.Request.Banner;
using Gico.SystemModels.Response.Banner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BannerItemController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IBannerAppService _bannerAppService;

        public BannerItemController(ILogger<BannerItemController> logger, IBannerAppService bannerAppService)
        {
            _logger = logger;
            _bannerAppService = bannerAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] BannerItemSearchRequest request)
        {
            try
            {
                BannerItemSearchResponse response = new BannerItemSearchResponse();
                ValidationResult validate = BannerItemSearchRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.SearchBannerItem(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]BannerItemGetRequest request)
        {
            BannerItemGetResponse response = new BannerItemGetResponse();
            try
            {
                ValidationResult validate = BannerItemGetRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.GetBannerItemById(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]BannerItemAddOrChangeRequest request)
        {
            BannerItemAddOrChangeResponse response = new BannerItemAddOrChangeResponse();
            try
            {
                ValidationResult validate = BannerItemAddRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.BannerItemAddOrChange(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Change([FromBody]BannerItemAddOrChangeRequest request)
        {
            BannerItemAddOrChangeResponse response = new BannerItemAddOrChangeResponse();
            try
            {
                ValidationResult validate = BannerItemChangeRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.BannerItemAddOrChange(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody]BannerItemRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = BannerItemRemoveRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.BannerItemRemove(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                }
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
