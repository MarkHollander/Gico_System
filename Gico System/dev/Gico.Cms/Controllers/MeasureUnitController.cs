using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Gico.SystemAppService.Implements;
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
    public class MeasureUnitController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMeasureUnitAppService _measureUnitAppService;
        public MeasureUnitController(ILogger<MeasureUnitController> logger, IMeasureUnitAppService measureUnitAppService)
        {
            _logger = logger;
            _measureUnitAppService = measureUnitAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] MeasureUnitSearchRequest request)
        {
            try
            {
                var response = await _measureUnitAppService.Search(request);
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
        public async Task<IActionResult> Add([FromBody] MeasureUnitAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = MeasureUnitAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _measureUnitAppService.Add(request);
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
        public async Task<IActionResult> Change([FromBody] MeasureUnitChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = MeasureUnitChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _measureUnitAppService.Change(request);
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