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
    public class CustomerController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ICustomerAppService _customerAppService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerAppService customerAppService)
        {
            _logger = logger;
            _customerAppService = customerAppService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] CustomerSearchRequest request)
        {
            try
            {
                var response = await _customerAppService.Search(request);
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
        public async Task<IActionResult> Get([FromBody] CustomerGetRequest request)
        {
            try
            {
                var response = await _customerAppService.Get( request);
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
        public async Task<IActionResult> Add([FromBody] CustomerAddOrChangeRequest request)
        {
            try
            {
                CustomerAddOrChangeResponse response = new CustomerAddOrChangeResponse();
                ValidationResult validate = CustomerAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _customerAppService.AddOrChange(request);
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
        public async Task<IActionResult> Change([FromBody] CustomerAddOrChangeRequest request)
        {
            try
            {
                CustomerAddOrChangeResponse response = new CustomerAddOrChangeResponse();
                var validate = CustomerChangeRequestValidator.ValidateModel(request);

                if (validate.IsValid)
                {
                    response = await _customerAppService.AddOrChange(request);
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