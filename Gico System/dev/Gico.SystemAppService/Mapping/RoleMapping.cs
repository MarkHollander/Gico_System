using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Mapping
{
    public static class RoleMapping
    {
        public static RoleViewModel ToModel(this RRole role, RDepartment department)
        {
            if (role == null)
            {
                return null;
            }
            return new RoleViewModel()
            {
                Name = role.Name,
                Id = role.Id,
                Status = role.Status == EnumDefine.RoleStatusEnum.Active,
                StatusName = role.Status.ToString(),
                DepartmentId = role.DepartmentId,
                DepartmentName = department?.Name
            };
        }

        public static RoleAddCommand ToCommand(this RoleAddRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new RoleAddCommand()
            {
                Name = request.Name,
                Id = Common.Common.GenerateGuid(),
                Status = request.Status ? EnumDefine.RoleStatusEnum.Active : EnumDefine.RoleStatusEnum.Deleted,
                DepartmentId = request.DepartmentId
            };
        }
        public static RoleChangeCommand ToCommand(this RoleChangeRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new RoleChangeCommand()
            {
                Name = request.Name,
                Id = request.Id,
                Status = request.Status ? EnumDefine.RoleStatusEnum.Active : EnumDefine.RoleStatusEnum.Deleted,
                DepartmentId = request.DepartmentId
            };
        }
        public static RoleActionMappingChangeByRoleCommand ToCommand(this PermissionChangeByRoleRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new RoleActionMappingChangeByRoleCommand()
            {
               RoleId = request.RoleId,
               ActionIdsAdd = request.ActionIdsAdd,
               ActionIdsRemove = request.ActionIdsRemove,
               
            };
        }
    }
}