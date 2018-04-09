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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ManufacturerCategoryMappingController:Controller
    {
        private readonly ILogger _logger;
        private readonly IManufacturerCategoryMappingAppService _manufacturerCategoryMappingAppService;

        public ManufacturerCategoryMappingController(ILogger<AttrCategoryController> logger, IManufacturerCategoryMappingAppService manufacturerCategoryMappingAppService)
        {
            _logger = logger;
            _manufacturerCategoryMappingAppService = manufacturerCategoryMappingAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] ManufacturerCategoryMappingRemoveRequest request)
        {
            try
            {
                ManufacturerCategoryMappingRemoveResponse response = new ManufacturerCategoryMappingRemoveResponse();
                var results = ManufacturerCategoryMappingRemoveRequestValidator.ValidateModel(request);
                if (results.IsValid)
                    response = await _manufacturerCategoryMappingAppService.ManufacturerCategoryMappingRemove(request);

                else
                    response.SetFail(results.Errors.Select(p => p.ToString()));

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
        public async Task<IActionResult> Gets([FromBody]ManufacturerMapping_GetManufacturerRequest request)
        {
            try
            {
                var response = await _manufacturerCategoryMappingAppService.Gets(request);
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
        public async Task<IActionResult> Add([FromBody] ManufacturerMappingAddRequest request)
        {
            try
            {
                ManufacturerMappingAddResponse response = new ManufacturerMappingAddResponse();
                var results = ManufacturerCategoryMappingAddRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    results = ManufacturerCategoryMappingAddRequestValidator.ValidateModel(request);
                    if (results.IsValid)
                    {
                        response = await _manufacturerCategoryMappingAppService.Add(request);
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


    }
}
