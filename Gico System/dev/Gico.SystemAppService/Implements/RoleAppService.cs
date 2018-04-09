using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Mapping;
using Gico.SystemDomains;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gico.SystemAppService.Implements
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IRoleService _roleService;
        private readonly ICurrentContext _context;
        private readonly ILogger<RoleAppService> _logger;
        private readonly ICommonService _commonService;

        public RoleAppService(IRoleService roleService, ICurrentContext context, ILogger<RoleAppService> logger, ICommonService commonService)
        {
            _roleService = roleService;
            _context = context;
            _logger = logger;
            _commonService = commonService;
        }

        public async Task<DepartmentSearchResponse> Search(DepartmentSearchRequest request)
        {
            DepartmentSearchResponse response = new DepartmentSearchResponse();
            try
            {
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                RDepartment[] departments = await _roleService.DepartmentSearchFromDb(request.Name, request.Status, sqlPaging);
                response.TotalRow = sqlPaging.TotalRow;
                response.Departments = departments.Select(p => p.ToModel()).ToArray();
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<DepartmentGetResponse> Get(DepartmentGetRequest request)
        {
            DepartmentGetResponse response = new DepartmentGetResponse();
            //var userLogin = await _context.GetCurrentCustomer();
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    RDepartment department = await _roleService.DepartmentGetFromDb(request.Id);
                    if (department == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.DepartmentNotFound);
                        return response;
                    }
                    response.Department = department.ToModel();
                }
                else
                {
                    response.Department = new DepartmentViewModel()
                    {
                        Status = false,
                    };
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> Add(DepartmentAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var command = request.ToCommand();
                CommandResult result = await _roleService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> Change(DepartmentChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var command = request.ToCommand();
                var result = await _roleService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<RoleSearchResponse> Search(RoleSearchRequest request)
        {
            RoleSearchResponse response = new RoleSearchResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, 30);
                RRole[] roles = await _roleService.RoleSearch(request.Name, request.Status, request.DepartmentId, paging);
                var departments = await _roleService.DepartmentSearchFromDb(string.Empty, 0, new RefSqlPaging(0, Int32.MaxValue));
                if (roles != null && roles.Length > 0)
                {
                    //    RDepartment[] departments = await _roleService.DepartmentGetFromDb(roles.Select(p => p.Id).ToArray());
                    response.Roles = roles.Select(p => p.ToModel(departments.FirstOrDefault(q => q.Id == p.DepartmentId))).ToArray();
                }
                else
                {
                    response.Roles = new RoleViewModel[0];
                }
                response.Departments = departments.Select(p => p.ToKeyValueTypeStringModel()).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<RoleGetResponse> Get(RoleGetRequest request)
        {
            RoleGetResponse response = new RoleGetResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    RRole role = await _roleService.RoleGetByIdFromDb(request.Id);
                    if (role == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.RoleNotFound);
                        return response;
                    }
                    response.Role = role.ToModel(null);
                }
                else
                {
                    response.Role = new RoleViewModel()
                    {
                        Status = false,
                        Id = string.Empty,
                        Name = string.Empty,
                        DepartmentId = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> Add(RoleAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var command = request.ToCommand();
                CommandResult result = await _roleService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> Change(RoleChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var command = request.ToCommand();
                var result = await _roleService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ActionDefineSearchResponse> Search(ActionDefineSearchRequest request)
        {
            ActionDefineSearchResponse response = new ActionDefineSearchResponse();
            try
            {
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, 30);
                RActionDefine[] actionDefines = await _roleService.ActionDefineGet(request.Group, string.Empty, sqlPaging);
                RRoleActionMapping[] roleActionMappings =
                    await _roleService.RoleActionMappingGetByRoleIdFromDb(request.RoleId);

                response.TotalRow = sqlPaging.TotalRow;
                response.ActionDefines = actionDefines.Select(p => p.ToModel(roleActionMappings.Any(q => q.ActionId == p.Id))).GroupBy(p => p.Group).Select(p => new KeyValuePair<string, ActionDefineViewModel[]>(p.Key, p.OrderBy(q => q.Name).ToArray())).ToArray();
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> PermissionChangeByRole(PermissionChangeByRoleRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var command = request.ToCommand();
                CommandResult result = await _roleService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
        public async Task<RoleActionMappingsResponse> PermissionGetAll()
        {
            RoleActionMappingsResponse response = new RoleActionMappingsResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                RRoleActionMapping[] roleActionMappings = await _roleService.RoleActionMappingGetByCustomerIdFromDb(user.Id);
                response.ActionIds = roleActionMappings.Select(p => p.ActionId).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message);
            }
            return response;
        }
    }
}