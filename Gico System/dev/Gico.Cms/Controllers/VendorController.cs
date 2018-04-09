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
    public class VendorController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IVendorAppService _vendorAppService;
        public VendorController(ILogger<VendorController> logger, IVendorAppService vendorAppService)
        {
            _logger = logger;
            _vendorAppService = vendorAppService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] VendorSearchRequest request)
        {
            try
            {
                var response = await _vendorAppService.Search(request);
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
        public async Task<IActionResult> Get([FromBody] VendorGetRequest request)
        {
            try
            {
                var response = await _vendorAppService.Get( request);
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
        public async Task<IActionResult> Add([FromBody] VendorAddOrChangeRequest request)
        {
            try
            {
                VendorAddOrChangeResponse response = new VendorAddOrChangeResponse();
                ValidationResult validate = VendorAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _vendorAppService.AddOrChange(request);
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
        public async Task<IActionResult> Change([FromBody] VendorAddOrChangeRequest request)
        {
            try
            {
                VendorAddOrChangeResponse response = new VendorAddOrChangeResponse();
                var validate = VendorChangeRequestValidator.ValidateModel(request);

                if (validate.IsValid)
                {
                    response = await _vendorAppService.AddOrChange(request);
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