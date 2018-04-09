using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;
using Gico.Common;
using Gico.Config;
using Gico.FrontEnd.Validations;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndModels.Models;
using Gico.Resilience.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEnd.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ICustomerAppService _customerAppService;
        public AccountController(ICustomerAppService customerAppService, ILogger<AccountController> logger) : base(logger)
        {
            _customerAppService = customerAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (await _customerAppService.IsAuthenticated())
            {
                return Redirect(SystemDefine.HomePage);
            }
            RegisterViewModel model = await _customerAppService.Register();
            return PartialView(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (await _customerAppService.IsAuthenticated())
            {
                return Redirect(SystemDefine.HomePage);
            }
            var validator = RegisterViewModelValidator.ValidateModel(model);
            model = await _customerAppService.Register(model);
            if (!model.IsSuccess && model.Messages.Count > 0)
            {
                foreach (var message in model.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState, null, model);
            }
            if (validator.IsValid && model.IsSuccess)
            {
                //await SetLogin(model.EmailOrMobile, false);
                return RedirectToAction("RegisterSuccess");
            }
            return PartialView(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RegisterSuccess()
        {
            RegisterSuccessViewModel model = await _customerAppService.RegisterSucces();
            if (!model.IsSuccess)
            {
                return NotFound();
            }
            return PartialView(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string returnurl)
        {
            string redirectUrl = returnurl;
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = SystemDefine.HomePage;
            }
            if (await _customerAppService.IsAuthenticated())
            {
                return Redirect(redirectUrl);
            }
            ViewData["returnurl"] = redirectUrl;
            LoginViewModel model = await _customerAppService.Login();
            return PartialView(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model, string returnurl)
        {
            string redirectUrl = returnurl;
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = SystemDefine.HomePage;
            }
            if (await _customerAppService.IsAuthenticated())
            {
                return Redirect(redirectUrl);
            }
            var validator = LoginViewModelValidator.ValidateModel(model);
            if (validator.IsValid)
            {
                model = await _customerAppService.Login(model);
            }
            if (!model.IsSuccess && model.Messages.Count > 0)
            {
                foreach (var message in model.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            if (model.IsSuccess)
            {
                ViewBag.RedirectUrl = redirectUrl;
            }
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState, null, model);
            }
            ViewData["returnurl"] = redirectUrl;
            return PartialView(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (await _customerAppService.IsAuthenticated())
            {
                await _customerAppService.Logout();
                return PartialView();
            }
            return NotFound();

        }

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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = SystemDefine.HomePage;
            }
            var redirectUrl = $"/ExternalLoginCallback?returnUrl={returnUrl}";
            return Challenge(new AuthenticationProperties() { RedirectUri = redirectUrl }, provider);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "")
        {
            try
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = SystemDefine.HomePage;
                }
                var model = await _customerAppService.ExternalLoginCallback();
                switch (model.Status)
                {
                    case EnumDefine.CustomerExternalLoginCallbackStatusEnum.LoginSuccess:
                        return Redirect(returnUrl);
                    case EnumDefine.CustomerExternalLoginCallbackStatusEnum.AccountNotExist:
                        return Redirect(returnUrl);
                    case EnumDefine.CustomerExternalLoginCallbackStatusEnum.AccountIsExist:
                        return PartialView(model);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, Common.Common.GetMethodName());
                throw;
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ExternalLoginSendActiveCode()
        {
            try
            {
                var model = await _customerAppService.SendActiveCodeWhenAccountIsExist();
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, Common.Common.GetMethodName());
                throw;
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ExternalLoginActive(ActiveWhenAccountIsExistModel model)
        {
            try
            {
                var result = await _customerAppService.ActiveWhenAccountIsExist(model);
                return Json(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, Common.Common.GetMethodName());
                throw;
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirm([FromForm] ExternalLoginConfirmViewModel model, string returnurl)
        {
            string redirectUrl = returnurl;
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = SystemDefine.HomePage;
            }
            if (await _customerAppService.IsAuthenticated())
            {
                return Redirect(redirectUrl);
            }
            var validator = ExternalLoginConfirmModelValidator.ValidateModel(model);
            if (validator.IsValid)
            {
                model = await _customerAppService.ExternalLoginConfirm(model);
            }
            if (!model.IsSuccess && model.Messages.Count > 0)
            {
                foreach (var message in model.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            if (model.IsSuccess)
            {
                ViewBag.RedirectUrl = redirectUrl;
            }
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState, null, model);
            }
            ViewData["returnurl"] = redirectUrl;
            return View("ExternalLoginCallback", model);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> VerifyExternalLoginWhenAccountIsExist(string verify, string code)
        {
            try
            {
                var model = new VerifyExternalLoginWhenAccountIsExistModel()
                {
                    VerifyId = verify,
                    VerifyCode = code
                };
                var validator = VerifyExternalLoginWhenAccountIsExistModelValidator.ValidateModel(model);
                if (validator.IsValid)
                {
                    model = await _customerAppService.VerifyExternalLoginWhenAccountIsExist(model);
                }
                if (!model.IsSuccess && model.Messages.Count > 0)
                {
                    foreach (var message in model.Messages)
                    {
                        ModelState.AddModelError(string.Empty, message);
                    }
                }
                if (!validator.IsValid)
                {
                    validator.AddToModelState(ModelState, null, model);
                }
                return PartialView(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, Common.Common.GetMethodName());
                throw;
            }
            
        }
    }

}