using System;
using System.Linq;
using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Gico.SystemAppService.Filters;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Response;
using Gico.SystemModels.Response.Banner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    public class AccountController : BaseController
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {
            try
            {
                RegisterResponse response = new RegisterResponse();
                var results = RegisterRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    //response = await _userAppService.Register(request);
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
        public async Task<IActionResult> CheckLogin()
        {
            BaseResponse response = new BannerItemSearchResponse();
            response.Status = await _accountAppService.CheckLogin();
            return Json(response);
        }
    }
}