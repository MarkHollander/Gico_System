using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class LocaleStringResourceController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ILocaleStringResourceAppService _localeStringResourceAppService;
        public LocaleStringResourceController(ILogger<LocaleStringResourceController> logger, ILocaleStringResourceAppService localeStringResourceAppService)
        {
            _logger = logger;
            _localeStringResourceAppService = localeStringResourceAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] LocaleStringResourceSearchRequest request)
        {
            try
            {
                var response = await _localeStringResourceAppService.Search(request);
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
        public async Task<IActionResult> Get([FromBody] LocaleStringResourceGetRequest request)
        {
            try
            {
                var response = await _localeStringResourceAppService.Get(request);
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
        public async Task<IActionResult> Add([FromBody] LocaleStringResourceAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = LocaleStringResourceAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _localeStringResourceAppService.Add(request);
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Change([FromBody] LocaleStringResourceChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = LocaleStringResourceChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _localeStringResourceAppService.Change(request);
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