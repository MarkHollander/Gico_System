using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.EmailOrSmsAppService.Interfaces;
using Gico.EmailOrSmsModel.Request;
using Gico.Models.Response;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class EmailOrSmsController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IEmailSmsAppService _emailSmsAppService;
        public EmailOrSmsController(ILogger<EmailOrSmsController> logger, IEmailSmsAppService emailSmsAppService)
        {
            _logger = logger;
            _emailSmsAppService = emailSmsAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] EmailOrSmsSearchRequest request)
        {
            try
            {
                var response = await _emailSmsAppService.Search(request);
                return Json(response);
            }
            catch(Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetDetail([FromBody] EmailOrSmsGetRequest request)
        {
            try
            {
                var response = await _emailSmsAppService.GetDetail(request);
                return Json(response);
            }
            catch(Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetVerifyDetail([FromBody] VerifyGetRequest request)
        {
            try
            {
                var response = await _emailSmsAppService.GetVerifyDetail(request);
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
