using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
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
    public class LanguageController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ILanguageAppService _languageAppService;
        public LanguageController(ILogger<LanguageController> logger, ILanguageAppService languageAppService)
        {
            _logger = logger;
            _languageAppService = languageAppService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] LanguageSearchRequest request)
        {
            try
            {
                var response = await _languageAppService.Search(request);
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
        public async Task<IActionResult> Add([FromBody] LanguageAddOrChangeRequest request)
        {
            try
            {
                LanguageAddOrChangeResponse response = new LanguageAddOrChangeResponse();
                ValidationResult validate = LanguageAddOrChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _languageAppService.AddOrChange(request);
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
        public async Task<IActionResult> Change([FromBody] LanguageAddOrChangeRequest request)
        {
            try
            {
                LanguageAddOrChangeResponse response = new LanguageAddOrChangeResponse();
                var validate = LanguageChangeRequestValidator.ValidateModel(request);

                if (validate.IsValid)
                {
                    response = await _languageAppService.AddOrChange(request);
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