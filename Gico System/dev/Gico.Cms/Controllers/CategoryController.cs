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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[EnableCors("CorsPolicy")]
    //[Produces("application/json")]
    //[Route("api/[controller]/[action]")]
    public class CategoryController : BaseController
    {

        private readonly ILogger _logger;
        private readonly ICategoryAppService _categoryAppService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryAppService categoryAppService)
        {
            _logger = logger;
            _categoryAppService = categoryAppService;
        }


        [HttpPost]
        public async Task<IActionResult> Gets([FromBody] CategoryGetsRequest request)
        {
            try
            {
                var response = await _categoryAppService.CategoryGet(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] CategoryGetRequest request)
        {
            try
            {
                var response = await _categoryAppService.CategoryGet(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrChange([FromBody] CategoryAddOrChangeRequest request)
        {
            try
            {
                CategoryAddOrChangeResponse response = new CategoryAddOrChangeResponse();
                var results = CategoryAddOrChangeRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    results = CategoryModelAddModelValidator.ValidateModel(request.Category);
                    if (results.IsValid)
                    {
                        response = await _categoryAppService.CategoryAddOrChange(request);
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

        [HttpPost]
        public async Task<IActionResult> GetListAttr([FromBody] CategoryAttrRequest request)
        {
            try
            {
                var response = await _categoryAppService.GetListAttr(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetListManufacturer([FromBody] CategoryManufacturerGetListRequest request)

        {
            try
            {
                var response = await _categoryAppService.GetListManufacturer(request);
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