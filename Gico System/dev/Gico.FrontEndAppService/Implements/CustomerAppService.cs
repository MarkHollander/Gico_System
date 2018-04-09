using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Common;
using Gico.Config;
using Gico.EmailOrSmsCommands;
using Gico.EmailOrSmsDomains;
using Gico.EmailOrSmsService.Interfaces;
using Gico.EmailSmsTemplateModels;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndAppService.Mapping;
using Gico.FrontEndModels.Models;
using Gico.ReadEmailSmsModels;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Gico.FrontEndAppService.Implements
{
    public class CustomerAppService : PageAppService, ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IRoleService _roleService;
        private readonly ICommonService _commonService;
        private readonly IEmailSmsService _emailSmsService;
        public CustomerAppService(IMenuService menuService,
            ILocaleStringResourceCacheStorage localeStringResourceCacheStorage,
            ILogger<CustomerAppService> logger,
            ICustomerService customerService,
            ICommonService commonService,
            ICurrentContext currentContext, IRoleService roleService, IEmailSmsService emailSmsService)
            : base(menuService, localeStringResourceCacheStorage, currentContext, logger)
        {
            _customerService = customerService;
            _commonService = commonService;
            _roleService = roleService;
            _emailSmsService = emailSmsService;
        }

        public async Task<RegisterViewModel> Register()
        {
            try
            {
                var page = await base.InitPage();
                RegisterViewModel model = new RegisterViewModel(page);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
        public async Task<RegisterViewModel> Register(RegisterViewModel model)
        {
            try
            {
                var page = await base.InitPage();
                model.SetInitInfo(page);
                var user = await _customerService.GetFromDbByEmailOrMobile(model.EmailOrMobile);
                if (user != null)
                {
                    model.AddMessage(ResourceKey.Account_Register_UserExist);
                    return model;
                }
                long systemNumericalOrder = await _commonService.GetNextId(typeof(Customer));
                var command = model.ToCommand(_currentContext.Ip, systemNumericalOrder);
                var result = await _customerService.SendCommand(command);
                if (!result.IsSucess)
                {
                    model.AddMessage(result.Message);
                }
                else
                {
                    await _commonService.SetFlagRegisterSuccessAsync(result.ObjectId);
                }
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, model);
                throw e;
            }
        }
        public async Task<RegisterSuccessViewModel> RegisterSucces()
        {
            try
            {
                var page = await base.InitPage();
                RegisterSuccessViewModel model = new RegisterSuccessViewModel(page);
                var customer = await _currentContext.GetCurrentCustomer();
                var haskey = await _commonService.GetFlagRegisterSuccessAsync(customer.Id);
                model.IsSuccess = haskey;
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
        public async Task<LoginViewModel> Login()
        {
            try
            {
                var page = await base.InitPage();
                LoginViewModel model = new LoginViewModel(page);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
        public async Task<LoginViewModel> Login(LoginViewModel model)
        {
            try
            {
                var page = await base.InitPage();
                model.SetInitInfo(page);
                var customer = await _customerService.GetFromDbByEmailOrMobile(model.UserName);
                if (customer == null)
                {
                    model.AddMessage(ResourceKey.Account_Login_FailMessage);
                    return model;
                }
                if (!await _customerService.ComparePassword(customer, model.Password))
                {
                    model.AddMessage(ResourceKey.Account_Login_FailMessage);
                    return model;
                }
                if (customer.Status == EnumDefine.CustomerStatusEnum.Lock)
                {
                    model.AddMessage(ResourceKey.Account_Login_AccountIsLock);
                    return model;
                }
                await Login(model.UserName, model.Remember, customer);
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, model);
                throw e;
            }
        }
        private async Task Login(string username, bool remember, RCustomer user)
        {
            try
            {
                string uniqueName = Common.Common.GenerateGuid();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, username),
                    new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName),
                    new Claim(SystemDefine.GicoAuId, uniqueName)
                };
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettingEnum.JwtTokensKey.GetConfig()));
                //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                //JwtSecurityToken token = new JwtSecurityToken(ConfigSettingEnum.JwtTokensIssuer.GetConfig(),
                //    ConfigSettingEnum.JwtTokensAudience.GetConfig(),
                //    claims,
                //    expires: Extensions.GetCurrentDateUtc().AddMinutes(model.Remember ?
                //                                                                            ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt() + 60 :
                //                                                                            ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt()),
                //    signingCredentials: creds);
                //string tokenKey = new JwtSecurityTokenHandler().WriteToken(token);
                //response.TokenKey = new JwtSecurityTokenHandler().WriteToken(token);
                //response.IsAdministrator = true;
                //RRoleActionMapping[] roleActionMappings = await _roleService.RoleActionMappingGetByCustomerIdFromDb(user.Id);
                //response.ActionIds = roleActionMappings.Select(p => p.ActionId).ToArray();
                await _customerService.SetLoginToCache(uniqueName, user);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                await _currentContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = Extensions.GetCurrentDateUtc().AddMinutes(remember ? ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt() + 60
                        : ConfigSettingEnum.LoginExpiresTime.GetConfig().AsInt())
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

        }
        public async Task Logout()
        {
            if (await IsAuthenticated())
            {
                await _currentContext.Logout();
            }
        }
        public async Task<ExternalLoginCallbackViewModel> ExternalLoginCallback()
        {
            try
            {
                var page = await base.InitPage();
                ExternalLoginCallbackViewModel model = new ExternalLoginCallbackViewModel(page);
                var user = _currentContext.HttpContext.User;
                if (user == null || !user.Identities.Any(identity => identity.IsAuthenticated))
                {
                    model.AddMessage(ResourceKey.Account_ExternalLoginCallback_Fail);
                    model.Status = EnumDefine.CustomerExternalLoginCallbackStatusEnum.LoginFail;
                    return model;
                }
                string name = user.Identity.Name.Trim();
                var identifier = user.FindFirst(p => p.Type == ClaimTypes.NameIdentifier)?.Value.Trim();
                var email = user.FindFirst(p => p.Type == ClaimTypes.Email)?.Value.Trim();
                model.FullName = name;
                model.EmailOrMobile = email;
                model.Identifier = identifier;
                var res = (EnumDefine.CutomerExternalLoginProviderEnum)Enum.Parse(typeof(EnumDefine.CutomerExternalLoginProviderEnum), user.Identity.AuthenticationType);
                model.LoginProvider = res;
                var customer = await _customerService.GetFromDbByEmailOrMobile(email);
                if (customer == null)
                {
                    model.Status = EnumDefine.CustomerExternalLoginCallbackStatusEnum.AccountNotExist;
                    long systemNumericalOrder = await _commonService.GetNextId(typeof(Customer));
                    var command = model.ToCommand(_currentContext.Ip, systemNumericalOrder);
                    var result = await _customerService.SendCommand(command);
                    if (!result.IsSucess)
                    {
                        model.AddMessage(result.Message);
                    }
                    else
                    {
                        customer = await _customerService.GetFromDbByEmailOrMobile(email);
                        await _commonService.SetFlagRegisterSuccessAsync(result.ObjectId);
                        await Login(model.EmailOrMobile, false, customer);
                    }
                    return model;
                }
                if (customer.CustomerExternalLogins != null && customer.CustomerExternalLogins.Any(p => p.LoginProvider == res && p.ProviderKey == model.Identifier))
                {
                    await Login(model.EmailOrMobile, false, customer);
                    model.Status = EnumDefine.CustomerExternalLoginCallbackStatusEnum.LoginSuccess;
                    return model;
                }
                else
                {
                    await Logout();
                    model.Status = EnumDefine.CustomerExternalLoginCallbackStatusEnum.AccountIsExist;
                    await SendActiveCodeWhenAccountIsExist();
                    return model;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }

        }
        public async Task<SendActiveCodeWhenAccountIsExistModel> SendActiveCodeWhenAccountIsExist()
        {
            try
            {
                SendActiveCodeWhenAccountIsExistModel model = new SendActiveCodeWhenAccountIsExistModel();
                var page = await base.InitPage();
                model.SetInitInfo(page);
                var user = _currentContext.HttpContext.User;
                if (user == null || !user.Identities.Any(identity => identity.IsAuthenticated))
                {
                    model.AddMessage(ResourceKey.Account_ExternalLoginCallback_Fail);
                    return model;
                }
                var identifier = user.FindFirst(p => p.Type == ClaimTypes.NameIdentifier)?.Value.Trim();
                var email = user.FindFirst(p => p.Type == ClaimTypes.Email)?.Value.Trim();
                var res = (EnumDefine.CutomerExternalLoginProviderEnum)Enum.Parse(typeof(EnumDefine.CutomerExternalLoginProviderEnum), user.Identity.AuthenticationType);
                model.EmailOrMobile = email;
                var customer = await _customerService.GetFromDbByEmailOrMobile(email);
                var messageType = string.IsNullOrEmpty(model.Email) ? EnumDefine.EmailOrSmsMessageTypeEnum.Sms : EnumDefine.EmailOrSmsMessageTypeEnum.Email;
                EmailOrSmsBaseCommand command = new EmailOrSmsBaseCommand(EnumDefine.EmailOrSmsTypeEnum.ExternalLoginConfirmWhenAccountIsExist, messageType, model.Mobile, model.Email)
                {
                    VerifyAddCommand = new VerifyAddCommand(TimeSpan.FromDays(2), EnumDefine.VerifyTypeEnum.EmailUrl, new ActiveCodeWhenAccountIsExistModel(customer.Id, res, identifier, string.Empty)),
                    Model = new TestEmailSmsModel()
                    {
                        Name = "Tàu sân bay Mỹ neo đậu ở vịnh Đà Nẵng",
                        Url = "https://vnexpress.net/tin-tuc/thoi-su/tau-san-bay-my-neo-dau-o-vinh-da-nang-3718320.html"
                    }
                };
                var result = await _emailSmsService.SendCommand(command);
                if (!result.IsSucess)
                {
                    model.AddMessage(result.Message);
                }
                else
                {
                    await _commonService.SetFlagRegisterSuccessAsync(result.ObjectId);
                }
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
        public async Task<ActiveWhenAccountIsExistModel> ActiveWhenAccountIsExist(ActiveWhenAccountIsExistModel model)
        {
            try
            {
                var page = await base.InitPage();
                model.SetInitInfo(page);
                var user = _currentContext.HttpContext.User;
                if (user == null || !user.Identities.Any(identity => identity.IsAuthenticated))
                {
                    model.AddMessage(ResourceKey.Account_ExternalLoginCallback_Fail);
                    return model;
                }
                var email = user.FindFirst(p => p.Type == ClaimTypes.Email)?.Value.Trim();
                model.EmailOrMobile = email;
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
        public async Task<ExternalLoginConfirmViewModel> ExternalLoginConfirm(ExternalLoginConfirmViewModel model)
        {
            try
            {
                var page = await base.InitPage();
                model.SetInitInfo(page);

                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }

        }
        public async Task<VerifyExternalLoginWhenAccountIsExistModel> VerifyExternalLoginWhenAccountIsExist(VerifyExternalLoginWhenAccountIsExistModel model)
        {
            try
            {
                var page = await base.InitPage();
                model.SetInitInfo(page);
                RVerify verify = await _emailSmsService.GetVerifyFromDb(model.VerifyId);
                if (verify == null)
                {
                    model.AddMessage(ResourceKey.Verify_NotExist);
                    return model;
                }
                if (verify.CheckStatus(EnumDefine.VerifyStatusEnum.Used))
                {
                    model.AddMessage(ResourceKey.Verify_Used);
                    return model;
                }
                if (verify.CheckStatus(EnumDefine.VerifyStatusEnum.Cancel))
                {
                    model.AddMessage(ResourceKey.Verify_Cancel);
                    return model;
                }
                if (verify.ExpireDate < Extensions.GetCurrentDateUtc())
                {
                    model.AddMessage(ResourceKey.Verify_Expired);
                    return model;
                }
                RijndaelSimple rijndaelSimple = new RijndaelSimple();
                string verifyCode;
                try
                {
                    string code = UnicodeUtility.FromHexString(model.VerifyCode);
                    verifyCode = rijndaelSimple.Decrypt(code, verify.SaltKey);
                }
                catch (Exception e)
                {
                    model.AddMessage(ResourceKey.Verify_CodeNotExact);
                    return model;
                }
                if (verify.VerifyCode != verifyCode)
                {
                    model.AddMessage(ResourceKey.Verify_CodeNotExact);
                    return model;
                }
                ActiveCodeWhenAccountIsExistModel activeCodeWhenAccountIsExistModel =
                    verify.GetModel<ActiveCodeWhenAccountIsExistModel>();
                var customer = await _customerService.GetFromDb(activeCodeWhenAccountIsExistModel.CustomerId);
                CustomerExternalLoginAddCommand command = new CustomerExternalLoginAddCommand()
                {
                    LoginProvider = activeCodeWhenAccountIsExistModel.LoginProvider,
                    ProviderKey = activeCodeWhenAccountIsExistModel.ProviderKey,
                    ProviderDisplayName = activeCodeWhenAccountIsExistModel.LoginProvider.ToString(),
                    CustomerId = activeCodeWhenAccountIsExistModel.CustomerId,
                    Info = activeCodeWhenAccountIsExistModel.Info,
                    VerifyId = verify.Id,
                    Version = customer.Version
                };
                var result = await _customerService.SendCommand(command);
                if (!result.IsSucess)
                {
                    model.AddMessage(result.Message);
                }
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }


        }
    }
}