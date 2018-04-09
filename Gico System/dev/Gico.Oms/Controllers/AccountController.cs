using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.Common;
using Gico.Oms.Validations;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gico.Oms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAccountAppService _accountAppService;
        public AccountController(
            ILogger<AccountController> logger, IAccountAppService accountAppService)
        {
            _logger = logger;
            _accountAppService = accountAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] LoginRequest request)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                var results = LoginRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    //await SetLogin(request.Email, request.RememberMe);
                    response = await _accountAppService.Login(request);
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

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        //{
        //    try
        //    {
        //        RegisterResponse response = new RegisterResponse();
        //        var results = RegisterRequestValidator.ValidateModel(request);
        //        if (results.IsValid)
        //        {
        //            //response = await _userAppService.Register(request);
        //        }
        //        else
        //        {
        //            response.SetFail(results.Errors.Select(p => p.ToString()));
        //        }
        //        return Json(response);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, Common.Common.GetMethodName(), request);
        //        throw;
        //    }
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult AccessDenied()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        [HttpGet]
        public IActionResult DecryptCookie()
        {
            ViewData["Message"] = "This is the decrypt page";
            var user = HttpContext.User;        //User will be set to the ClaimsPrincipal

            //Get the encrypted cookie value
            string cookieValue = HttpContext.Request.Cookies["gicoOAU"];
            IDataProtectionProvider provider = HttpContext.RequestServices.GetService<IDataProtectionProvider>();

            //Get a data protector to use with either approach
            var dataProtector = provider.CreateProtector(
                "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                "Cookies",
                "v2");


            //Get the decrypted cookie as plain text
            UTF8Encoding specialUtf8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            byte[] protectedBytes = Base64UrlTextEncoder.Decode(cookieValue);
            byte[] plainBytes = dataProtector.Unprotect(protectedBytes);
            string plainText = specialUtf8Encoding.GetString(plainBytes);


            //Get teh decrypted cookies as a Authentication Ticket
            TicketDataFormat ticketDataFormat = new TicketDataFormat(dataProtector);
            AuthenticationTicket ticket = ticketDataFormat.Unprotect(cookieValue);

            return Content("111");
        }
    }
}