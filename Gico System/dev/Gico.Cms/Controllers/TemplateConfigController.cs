using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Gico.SystemModels.Request.PageBuilder;
using Gico.SystemAppService.Interfaces.PageBuilder;
using Gico.SystemModels.Response.PageBuilder;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TemplateConfigController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ITemplateConfigAppService _templateConfigAppService;

        public TemplateConfigController(ILogger<TemplateController> logger, ITemplateConfigAppService templateConfigAppService)
        {
            _logger = logger;
            _templateConfigAppService = templateConfigAppService;
        }
        [HttpPost]
        public async Task<IActionResult> Search([FromBody] TemplateConfigSearchRequest request)
        {
            try
            {
                var response = await _templateConfigAppService.Search(request);

                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]TemplateConfigGetRequest request)
        {
            try
            {
                var response = await _templateConfigAppService.GetTemplateConfigById(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TemplateConfigAddOrChangeRequest request)
        {
            try
            {
                TemplateConfigAddOrChangeResponse response = new TemplateConfigAddOrChangeResponse();
                ValidationResult validate = TemplateConfigAddRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateConfigAppService.TemplateConfigAddOrChange(request);
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
        public async Task<IActionResult> Change([FromBody]TemplateConfigAddOrChangeRequest request)
        {
            try
            {
                TemplateConfigAddOrChangeResponse response = new TemplateConfigAddOrChangeResponse();
                ValidationResult validate = TemplateConfigChangeRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateConfigAppService.TemplateConfigAddOrChange(request);
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
        public async Task<IActionResult> CheckCodeExist([FromBody] TemplateCheckCodeExistRequest request)
        {
            try
            {
                TemplateCheckCodeExistResponse response = new TemplateCheckCodeExistResponse();
                ValidationResult validate = TemplateCheckCodeExistRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateConfigAppService.TemplateConfigCheckCodeExist(request);
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

        [HttpGet]
        public async Task<IActionResult> SearchComponents([FromQuery] ComponentsAutocompleteRequest request)
        {
            try
            {
                ComponentsAutocompleteResponse response = new ComponentsAutocompleteResponse();
                ValidationResult validate = ComponentsAutocompleteRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateConfigAppService.ComponentsAutocomplete(request);
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
        public async Task<IActionResult> Remove([FromBody]TemplateConfigRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = TemplateConfigRemoveRequestValidate.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _templateConfigAppService.TemplateConfigRemove(request);
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
