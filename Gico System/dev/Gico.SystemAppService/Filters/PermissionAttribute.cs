using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.SystemCommands;
using Gico.SystemService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gico.SystemAppService.Filters
{

    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(string group, string name) : base(typeof(PermissionFilter))
        {
            this.Arguments = new object[] { group, name };
        }
    }
    public class PermissionFilter : IAsyncActionFilter
    {
        private readonly IRoleService _roleService;
        private readonly ICurrentContext _currentContext;
        public PermissionFilter(string group, string name, IRoleService roleService, ICurrentContext currentContext)
        {
            Group = group;
            Name = name;
            _roleService = roleService;
            _currentContext = currentContext;
        }
        private string Group { get; }
        private string Name { get; }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var actionName = descriptor.ActionName;
            var controllerName = descriptor.ControllerName;
            string key = $"{controllerName}/{actionName}";
            var t = AddIfNotExists(key);
            //// logic before action goes here
            var permission = await _currentContext.GetPermission(key);
            if (permission == null)
            {
                //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new ContentResult()
                {
                    Content = "Unauthorized",
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }
            else
            {
                await next(); // the actual action
            }
            // logic after the action goes here
        }

        private async Task AddIfNotExists(string key)
        {
            var actionKeyExist = await _roleService.CheckExists(key);
            if (actionKeyExist)
            {
                return;
            }

            await _roleService.SendCommandNoWait(new ActionDefineAddCommand
            {
                Id = key,
                Group = Group,
                Name = Name
            });
        }
    }

    public class AuthorizeTokenValidateAttribute : TypeFilterAttribute
    {
        public AuthorizeTokenValidateAttribute() : base(typeof(AuthorizeTokenValidateFilter))
        {
            this.Arguments = new object[] { };
        }
    }
    public class AuthorizeTokenValidateFilter : IAsyncActionFilter
    {
        private readonly ICurrentContext _currentContext;
        public AuthorizeTokenValidateFilter(ICurrentContext currentContext)
        {
            _currentContext = currentContext;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IList<FilterDescriptor> actionDescriptor = context.ActionDescriptor.FilterDescriptors;
            bool isCheckToken = true;
            foreach (var filterDescriptors in actionDescriptor)
            {
                if (filterDescriptors.Filter.GetType() == typeof(AllowAnonymousFilter))
                {
                    await next();
                    isCheckToken = false;
                    break;
                }
            }
            if (isCheckToken)
            {
                var valid = await _currentContext.CheckAuthenToken();
                if (valid)
                {
                    await next();
                }
                else
                {
                    context.Result = new ContentResult()
                    {
                        Content = "Unauthorized",
                        StatusCode = (int)HttpStatusCode.Unauthorized
                    };
                }
            }
            
        }
    }
}