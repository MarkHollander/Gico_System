using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;

namespace Gico.SystemService.Interfaces
{
    public interface IRoleService
    {
        #region READ From DB

        Task<RActionDefine> ActionDefineGetFromDb(string id);
        Task<RActionDefine[]> ActionDefineGetFromDb(string[] ids);
        Task<RActionDefine[]> ActionDefineGet(string group, string name, RefSqlPaging paging);
        Task<RRoleActionMapping[]> RoleActionMappingGetByCustomerIdFromDb(string customerId);
        Task<RRoleActionMapping[]> RoleActionMappingGetByRoleIdFromDb(string roleId);
        Task<RDepartment[]> DepartmentSearchFromDb(string name, EnumDefine.DepartmentStatusEnum status, RefSqlPaging sqlPaging);
        Task<RDepartment> DepartmentGetFromDb(string id);
        Task<RDepartment[]> DepartmentGetFromDb(string[] ids);
        Task<RRole[]> RoleGetByDepartmentIdFromDb(string departmentId);

        Task<RRole[]> RoleSearch(string name, EnumDefine.RoleStatusEnum status, string departmentId,
            RefSqlPaging sqlPaging);
        Task<RRole> RoleGetByIdFromDb(string id);
        #endregion

        #region Write To Db

        Task AddToDb(ActionDefine actionDefine);
        Task AddToDb(Department department);
        Task ChangeToDb(Department department);
        Task AddToDb(Role role);
        Task ChangeToDb(Role role);
        Task ChangeToDb(RoleActionMapping[] roleActionMappingsAdd, string roleId, string[] actionIds);
        #endregion

        #region Command

        Task SendCommandNoWait(ActionDefineAddCommand command);
        Task<CommandResult> SendCommand(ActionDefineAddCommand command);
        Task<CommandResult> SendCommand(DepartmentAddCommand command);
        Task<CommandResult> SendCommand(DepartmentChangeCommand command);
        Task<CommandResult> SendCommand(RoleAddCommand command);
        Task<CommandResult> SendCommand(RoleChangeCommand command);
        Task<CommandResult> SendCommand(RoleActionMappingChangeByRoleCommand command);
        #endregion

        #region Get From Cache

        Task<RActionDefine> ActionDefineGetFromCache(string id);
        Task<RActionDefine[]> ActionDefineGetFromCache(string[] ids);
        Task<bool> CheckExists(string id);
        #endregion

        #region Add To Cache

        Task AddToCache(RActionDefine action);

        #endregion



        #region Common

        #endregion


    }
}