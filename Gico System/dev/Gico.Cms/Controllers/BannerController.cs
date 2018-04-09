using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BannerController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IBannerAppService _bannerAppService;

        public BannerController(ILogger<BannerController> logger, IBannerAppService bannerAppService)
        {
            _logger = logger;
            _bannerAppService = bannerAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] BannerSearchRequest request)
        {
            try
            {
                var response = await _bannerAppService.SearchBanner(request);

                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]BannerGetRequest request)
        {
            try
            {
                var response = await _bannerAppService.GetBannerById(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]BannerAddOrChangeRequest request)
        {
            try
            {
                var response = new BannerAddOrChangeResponse();
                ValidationResult validate = BannerAddRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.BannerAddOrChange(request);
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
        public async Task<IActionResult> Change([FromBody]BannerAddOrChangeRequest request)
        {
            try
            {
                var response = new BannerAddOrChangeResponse();
                ValidationResult validate = BannerChangeRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.BannerAddOrChange(request);
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
        public async Task<IActionResult> Remove([FromBody]BannerRemoveRequest request)
        {
            try
            {
                var response = new BaseResponse();
                ValidationResult validate = BannerRemoveRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _bannerAppService.BannerRemove(request);
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
