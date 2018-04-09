using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Mapping
{
    public static class DepartmentMapping
    {
        public static DepartmentViewModel ToModel(this RDepartment department)
        {
            if (department == null)
            {
                return null;
            }
            return new DepartmentViewModel()
            {
                Name = department.Name,
                Id = department.Id,
                Status = department.Status == EnumDefine.DepartmentStatusEnum.Active,
                StatusName = department.Status.ToString()

            };
        }
        public static KeyValueTypeStringModel ToKeyValueTypeStringModel(this RDepartment department)
        {
            if (department == null)
            {
                return null;
            }
            return new KeyValueTypeStringModel()
            {
                Value = department.Id,
                Text = department.Name
            };
        }

        public static DepartmentAddCommand ToCommand(this DepartmentAddRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new DepartmentAddCommand()
            {
                Name = request.Name,
                Id = Common.Common.GenerateGuid(),
                Status = request.Status ? EnumDefine.DepartmentStatusEnum.Active : EnumDefine.DepartmentStatusEnum.Deleted,

            };
        }
        public static DepartmentChangeCommand ToCommand(this DepartmentChangeRequest request)
        {
            if (request == null)
            {
                return null;
            }
            return new DepartmentChangeCommand()
            {
                Name = request.Name,
                Id = request.Id,
                Status = request.Status ? EnumDefine.DepartmentStatusEnum.Active : EnumDefine.DepartmentStatusEnum.Deleted,

            };
        }
    }
}