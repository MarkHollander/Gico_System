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
using FluentValidation.Results;


namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ManufacturerController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IManufacturerAppService _manufacturerAppService;
        public ManufacturerController(ILogger<ManufacturerController> logger, IManufacturerAppService manufacturerAppService)
        {
            _logger = logger;
            _manufacturerAppService = manufacturerAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]ManufacturerGetRequest request)
        {
            try
            {
                if (String.IsNullOrEmpty(request.Name) && String.IsNullOrEmpty(request.Description))
                {
                    var response = await _manufacturerAppService.GetAll(request);
                    return Json(response);

                }
                else
                {
                    var response = await _manufacturerAppService.Search(request);
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetById([FromBody]ManufacturerGetRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id)) return Json(new ManufacturerGetResponse());
                else
                {
                    var response = await _manufacturerAppService.GetById(request);
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrChange([FromBody]ManufacturerManagementAddOrChangeRequest request)
        {
            try
            {
                ManufacturerGetResponse response = new ManufacturerGetResponse();
                ValidationResult validate = ManufacturerManagementAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _manufacturerAppService.AddOrChange(request);
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