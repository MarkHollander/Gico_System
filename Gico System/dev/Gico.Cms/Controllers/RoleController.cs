using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;
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
    public class RoleController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRoleAppService _roleAppService;

        public RoleController(IRoleAppService roleAppService, ILogger<RoleController> logger)
        {
            _roleAppService = roleAppService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody]DepartmentSearchRequest request)
        {
            try
            {
                var response = await _roleAppService.Search(request);
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
        public async Task<IActionResult> DepartmentGet([FromBody]DepartmentGetRequest request)
        {
            try
            {
                var response = await _roleAppService.Get(request);
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
        public async Task<IActionResult> DepartmentAdd([FromBody] DepartmentAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = DepartmentAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _roleAppService.Add(request);
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
        public async Task<IActionResult> DepartmentChange([FromBody] DepartmentChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = DepartmentChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _roleAppService.Change(request);
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
        public async Task<IActionResult> RoleSearch([FromBody] RoleSearchRequest request)
        {
            try
            {
                //ValidationResult validate = RoleGetByDepartmentIdRequestValidator.ValidateModel(request);
                var response = await _roleAppService.Search(request);
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
        public async Task<IActionResult> RoleAdd([FromBody] RoleAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = RoleAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _roleAppService.Add(request);
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
        public async Task<IActionResult> RoleChange([FromBody] RoleChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = RoleChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _roleAppService.Change(request);
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
        public async Task<IActionResult> ActionDefineSearch([FromBody] ActionDefineSearchRequest request)
        {
            try
            {
                var response = await _roleAppService.Search(request);
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
        public async Task<IActionResult> PermissionChangeByRole([FromBody] PermissionChangeByRoleRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = PermissionChangeByRoleRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _roleAppService.PermissionChangeByRole(request);
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
        public async Task<IActionResult> PermissionGet()
        {
            try
            {
                var response = await _roleAppService.PermissionGetAll();
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

    }
}