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
    public class AttrCategoryController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAttrCategoryAppService _attrCategoryAppService;
        public AttrCategoryController(ILogger<AttrCategoryController> logger, IAttrCategoryAppService attrCategoryAppService)
        {
            _logger = logger;
            _attrCategoryAppService = attrCategoryAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AttrCategoryAddRequest request)
        {
            try
            {
                AttrCategoryAddOrChangeResponse response = new AttrCategoryAddOrChangeResponse();
                var results = AttrCategoryAddRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    results = AttrCategoryModelAddModelValidator.ValidateModel(request.AttrCategory);
                    if (results.IsValid)
                    {
                        response = await _attrCategoryAppService.AttrCategoryAdd(request);
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Change([FromBody] AttrCategoryChangeRequest request)
        {
            try
            {
                AttrCategoryAddOrChangeResponse response = new AttrCategoryAddOrChangeResponse();
                var results = AttrCategoryChangeRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    results = AttrCategoryModelAddModelValidator.ValidateModel(request.AttrCategory);
                    if (results.IsValid)
                    {
                        response = await _attrCategoryAppService.AttrCategoryChange(request);
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] AttrCategoryRemoveRequest request)
        {
            try
            {
                AttrCategoryRemoveResponse response = new AttrCategoryRemoveResponse();
                var results = AttrCategoryRemoveRequestValidator.ValidateModel(request);
                if (results.IsValid)
                    response = await _attrCategoryAppService.AttrCategoryRemove(request);
                
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
        public async Task<IActionResult> Get([FromBody] AttrCategoryGetRequest request)
        {
            try
            {
                var response = await _attrCategoryAppService.AttrCategoryGet(request);
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
        public async Task<IActionResult> GetProductAttr([FromBody] AttrCategoryMapping_GetsProductAttrRequest request)
        {
            try
            {
                var response = await _attrCategoryAppService.GetsProductAttr(request);
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