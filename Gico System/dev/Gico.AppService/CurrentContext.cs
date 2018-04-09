using System;
using Gico.ReadSystemModels;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.Common;
using Gico.Config;
using Microsoft.AspNetCore.Http;
using Gico.SystemService.Interfaces;
using Microsoft.AspNetCore.Routing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using Extensions = Gico.Common.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Gico.AppService
{
    public interface ICurrentContext
    {
        string Ip { get; }
        //string LoginToken { get; }
        Task<RCustomer> GetCurrentCustomer();
        Task<IDictionary<string, RRoleActionMapping>> GetPermissionActions();
        Task<RRoleActionMapping> GetPermission(string actionkey);
        string LanguageId { get; }
        string ClientId { get; }
        Task<bool> IsAuthenticated();
        Task<bool> CheckAuthenToken();
        string GetParam(string key);
        HttpContext HttpContext { get; }
        Task Logout();
    }

    public class CurrentContext : ICurrentContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string _ip;
        private string _loginToken;
        private RCustomer _currentCustomer;
        private IDictionary<string, RRoleActionMapping> _actions;
        private string _sessionId;
        private string _clientId;

        private readonly ICustomerService _customerService;
        private readonly IRoleService _roleService;

        public CurrentContext(IHttpContextAccessor httpContextAccessor, ICustomerService customerService,
            IRoleService roleService)
        {
            _httpContextAccessor = httpContextAccessor;
            _customerService = customerService;
            _roleService = roleService;
        }

        public string Ip
        {
            get
            {
                if (string.IsNullOrEmpty(_ip))
                {
                    _ip = GetCurrentIpAddress();
                }
                return _ip;
            }
        }
        private bool IsRequestAvailable()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        private string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            var result = string.Empty;
            try
            {
                //first try to get IP address from the forwarded header
                if (_httpContextAccessor.HttpContext.Request.Headers != null)
                {
                    //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
                    //connecting to a web server through an HTTP proxy or load balancer
                    var forwardedHttpHeaderKey = "X-FORWARDED-FOR";
                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                        result = forwardedHeader.FirstOrDefault();
                }
                //if this header not exists try get connection remote IP address
                if (string.IsNullOrEmpty(result) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                    result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                return string.Empty;
            }
            if (result != null && result.Equals("::1", StringComparison.InvariantCultureIgnoreCase))
                result = "127.0.0.1";
            if (!string.IsNullOrEmpty(result))
                result = result.Split(':').FirstOrDefault();

            return result;
        }

        public async Task<RCustomer> GetCurrentCustomer()
        {
            if (await IsAuthenticated())
            {
                if (_currentCustomer != null)
                {
                    return _currentCustomer;
                }
                string key = _httpContextAccessor.HttpContext.User.FindFirst(SystemDefine.GicoAuId).Value;
                _currentCustomer = await _customerService.GetLoginInfoFromCache(key);
                return _currentCustomer;
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                throw new Exception("Unauthorized");
            }
        }

        public async Task<IDictionary<string, RRoleActionMapping>> GetPermissionActions()
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new ConcurrentDictionary<string, RRoleActionMapping>();
            }
            if (_actions != null)
            {
                return _actions;
            }
            var actions = await _roleService.RoleActionMappingGetByCustomerIdFromDb(customer.Id);
            _actions = actions.ToDictionary(p => p.Id, p => p);
            return _actions;
        }

        public async Task<RRoleActionMapping> GetPermission(string actionkey)
        {
            var actions = await GetPermissionActions();
            if (actions.TryGetValue(actionkey, out var roleActionMapping))
            {
                return roleActionMapping;
            }
            return null;
        }

        public string SessionId
        {
            get
            {
                if (string.IsNullOrEmpty(_sessionId))
                {
                    _sessionId = _httpContextAccessor.HttpContext.Session.Id;
                }
                return _sessionId;
            }
        }

        private RouteValueDictionary Params => _httpContextAccessor.HttpContext.GetRouteData().Values;

        public string LanguageId => GetParam(SystemDefine.Culture);

        public string CurrencyId => _httpContextAccessor.HttpContext.Request.Cookies[SystemDefine.CurrencyCookie];
        public string MenuId { get; set; }

        public async Task<bool> IsAuthenticated()
        {
            var isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            return await Task.FromResult(isAuthenticated);
        }

        public async Task<bool> CheckAuthenToken()
        {
            string key = _httpContextAccessor.HttpContext.User.FindFirst(SystemDefine.GicoAuId).Value;
            return await _customerService.CheckLoginInfoFromCache(key);
        }

        public string ClientId
        {
            get
            {
                if (string.IsNullOrEmpty(_clientId))
                {
                    if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(SystemDefine.ClientIdCookie, out var clientId))
                    {
                        _clientId = clientId;
                    }
                    else
                    {
                        _clientId = Common.Common.GenerateGuid();
                        _httpContextAccessor.HttpContext.Response.Cookies.Append(SystemDefine.ClientIdCookie, ClientId, new CookieOptions()
                        {
                            Expires = Extensions.GetCurrentDateUtc().AddYears(10),
                            HttpOnly = true
                        });
                    }
                }
                return _clientId;
            }
        }

        public string GetParam(string key)
        {
            if (Params.ContainsKey(key))
            {
                return Params[key].AsString();
            }
            return string.Empty;
        }

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        }

    }
}