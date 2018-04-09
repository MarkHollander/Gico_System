using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Gico.SystemModels.Request.PageBuilder;
using Gico.SystemAppService.Interfaces.PageBuilder;
using Gico.SystemModels.Response.PageBuilder;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TemplateController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ITemplateAppService _templateAppService;

        public TemplateController(ILogger<TemplateController> logger, ITemplateAppService templateAppService)
        {
            _logger = logger;
            _templateAppService = templateAppService;
        }
        [HttpPost]
        public async Task<IActionResult> Search([FromBody] TemplateSearchRequest request)
        {
            try
            {
                var response = await _templateAppService.Search(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]TemplateGetRequest request)
        {
            try
            {
                var response = await _templateAppService.GetTemplateById(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TemplateAddOrChangeRequest request)
        {
            try
            {
                TemplateAddOrChangeResponse response = new TemplateAddOrChangeResponse();
                ValidationResult validate = TemplateAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateAppService.TemplateAddOrChange(request);
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
        public async Task<IActionResult> Change([FromBody]TemplateAddOrChangeRequest request)
        {
            try
            {
                TemplateAddOrChangeResponse response = new TemplateAddOrChangeResponse();
                ValidationResult validate = TemplateChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateAppService.TemplateAddOrChange(request);
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
        public async Task<IActionResult> Remove([FromBody]TemplateRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = TemplateRemoveRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateAppService.TemplateRemove(request);
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
