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
    public class VariationThemeController:BaseController
    {
        private readonly ILogger _logger;
        private readonly IVariationThemeAppService _variationThemeAppService;

        public VariationThemeController(ILogger<VariationThemeController> logger, IVariationThemeAppService variationThemeAppService)
        {
            _logger = logger;
            _variationThemeAppService = variationThemeAppService;

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetVariationTheme([FromBody] VariationThemeGetRequest request)
        {
            try
            {
                var response = await _variationThemeAppService.VariationThemeGet(request);
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
        public async Task<IActionResult> GetVariationTheme_Attribute([FromBody] VariationTheme_AttributeGetRequest request)
        {
            try
            {
                var response = await _variationThemeAppService.VariationTheme_AttributeGet(request);
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
        public async Task<IActionResult> Add([FromBody] Category_VariationTheme_MappingAddRequest request)
        {
            try
            {
                Category_VariationTheme_MappingAddResponse response = new Category_VariationTheme_MappingAddResponse();
                var results = CategoryVariationThemeAddRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                        response = await _variationThemeAppService.Category_VariationTheme_MappingAdd(request);
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


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] Category_VariationTheme_Mapping_RemoveRequest request)
        {
            try
            {
                Category_VariationTheme_Mapping_RemoveResponse response = new Category_VariationTheme_Mapping_RemoveResponse();
                var results = CategoryVariationThemeRemoveRequestValidator.ValidateModel(request);
                if (results.IsValid)
                    response = await _variationThemeAppService.Remove(request);

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
        public async Task<IActionResult> Category_VariationTheme_MappingGets([FromBody] Category_VariationTheme_MappingGetsRequest request)
        {
            try
            {
                var response = await _variationThemeAppService.Category_VariationTheme_MappingGets(request);
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
