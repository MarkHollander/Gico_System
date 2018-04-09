using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.AppService;
using Gico.Common;
using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Gico.SystemAppService.Implements
{
    public class AccountAppService : IAccountAppService
    {
        private readonly ILogger<AccountAppService> _logger;
        private readonly ICustomerService _customerService;
        private readonly IRoleService _roleService;
        private readonly ICurrentContext _currentContext;
        public AccountAppService(ILogger<AccountAppService> logger, ICustomerService customerService, IRoleService roleService, ICurrentContext currentContext)
        {
            _logger = logger;
            _customerService = customerService;
            _roleService = roleService;
            _currentContext = currentContext;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                var user = await _customerService.GetFromDbByEmailOrMobile(request.Email);
                if (user == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNameOrPasswordNotcorrect);
                    return response;
                }
                if (!await _customerService.ComparePassword(user, request.Password))
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNameOrPasswordNotcorrect);
                    return response;
                }
                string uniqueName = Common.Common.GenerateGuid();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, request.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName),
                    new Claim(SystemDefine.GicoAuId, uniqueName)
                    
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettingEnum.JwtTokensKey.GetConfig()));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                JwtSecurityToken token = new JwtSecurityToken(ConfigSettingEnum.JwtTokensIssuer.GetConfig(),
                    ConfigSettingEnum.JwtTokensAudience.GetConfig(),
                    claims,
                    expires: Extensions.GetCurrentDateUtc().AddMinutes(request.RememberMe ?
                                                                                            ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt() + 60 :
                                                                                            ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt()),
                    signingCredentials: creds);
                response.TokenKey = new JwtSecurityTokenHandler().WriteToken(token);
                response.IsAdministrator = true;
                RRoleActionMapping[] roleActionMappings = await _roleService.RoleActionMappingGetByCustomerIdFromDb(user.Id);
                response.ActionIds = roleActionMappings.Select(p => p.ActionId).ToArray();
                await _customerService.SetLoginToCache(uniqueName, user);
                await SetLogin(request.RememberMe, claims);
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;

        }

        public async Task<bool> CheckLogin()
        {
            return await _currentContext.IsAuthenticated();
        }

        private async Task SetLogin(bool remember, Claim[] claims)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            await _currentContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = Extensions.GetCurrentDateUtc().AddMinutes(remember ? ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt() + 60
                                                                                : ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt())
            });
        }
    }
}