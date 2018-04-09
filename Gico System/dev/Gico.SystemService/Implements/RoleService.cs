using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemService.Interfaces;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemService.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IActionDefineRepository _actionDefineRepository;
        private readonly ICommandSender _commandService;
        private readonly IRoleCacheStorage _roleCacheStorage;
        private readonly IRoleActionMappingRepository _roleActionMappingRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRoleRepository _roleRepository;
        public RoleService(IActionDefineRepository actionDefineRepository, ICommandSender commandService, IRoleCacheStorage roleCacheStorage, IRoleActionMappingRepository roleActionMappingRepository, IDepartmentRepository departmentRepository, IRoleRepository roleRepository)
        {
            _actionDefineRepository = actionDefineRepository;
            _commandService = commandService;
            _roleCacheStorage = roleCacheStorage;
            _roleActionMappingRepository = roleActionMappingRepository;
            _departmentRepository = departmentRepository;
            _roleRepository = roleRepository;
        }

        #region READ From DB

        public async Task<RActionDefine> ActionDefineGetFromDb(string id)
        {
            return await _actionDefineRepository.Get(id);
        }
        public async Task<RActionDefine[]> ActionDefineGetFromDb(string[] ids)
        {
            return await _actionDefineRepository.Get(ids);
        }

        public async Task<RActionDefine[]> ActionDefineGet(string group, string name, RefSqlPaging paging)
        {
            return await _actionDefineRepository.Get(group, name, paging);
        }

        public async Task<RRoleActionMapping[]> RoleActionMappingGetByCustomerIdFromDb(string customerId)
        {
            return await _roleActionMappingRepository.GetByCustomerId(customerId);
        }

        public async Task<RRoleActionMapping[]> RoleActionMappingGetByRoleIdFromDb(string roleId)
        {
            return await _roleActionMappingRepository.GetByRoleId(roleId);
        }

        public async Task<RDepartment[]> DepartmentSearchFromDb(string name, EnumDefine.DepartmentStatusEnum status, RefSqlPaging sqlPaging)
        {
            return await _departmentRepository.Search(name, status, sqlPaging);
        }

        public async Task<RDepartment> DepartmentGetFromDb(string id)
        {
            return await _departmentRepository.Get(id);
        }

        public async Task<RDepartment[]> DepartmentGetFromDb(string[] ids)
        {
            return await _departmentRepository.Get(ids);
        }

        public async Task<RRole[]> RoleGetByDepartmentIdFromDb(string departmentId)
        {
            return await _roleRepository.GetByDepartmentId(departmentId);
        }

        public async Task<RRole[]> RoleSearch(string name, EnumDefine.RoleStatusEnum status, string departmentId, RefSqlPaging sqlPaging)
        {
            return await _roleRepository.Search(name, status, departmentId, sqlPaging);
        }

        public async Task<RRole> RoleGetByIdFromDb(string id)
        {
            return await _roleRepository.GetById(id);
        }

        #endregion

        #region Write To Db

        public async Task AddToDb(ActionDefine actionDefine)
        {
            await _actionDefineRepository.Add(actionDefine);
        }

        public async Task AddToDb(Department department)
        {
            await _departmentRepository.Add(department);
        }

        public async Task ChangeToDb(Department department)
        {
            await _departmentRepository.Change(department);
        }

        public async Task AddToDb(Role role)
        {
            await _roleRepository.Add(role);
        }

        public async Task ChangeToDb(Role role)
        {
            await _roleRepository.Change(role);
        }

        public async Task ChangeToDb(RoleActionMapping[] roleActionMappingsAdd, string roleId, string[] actionIds)
        {
            await _roleActionMappingRepository.Change(roleActionMappingsAdd, roleId, actionIds);
        }

        #endregion

        #region Command

        public async Task SendCommandNoWait(ActionDefineAddCommand command)
        {
            await _commandService.Send(command);
        }

        public async Task<CommandResult> SendCommand(ActionDefineAddCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        public async Task<CommandResult> SendCommand(DepartmentAddCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        public async Task<CommandResult> SendCommand(DepartmentChangeCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        public async Task<CommandResult> SendCommand(RoleAddCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        public async Task<CommandResult> SendCommand(RoleChangeCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }
        public async Task<CommandResult> SendCommand(RoleActionMappingChangeByRoleCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }
        #endregion

        #region Get From Cache

        public async Task<RActionDefine> ActionDefineGetFromCache(string id)
        {
            return await _roleCacheStorage.ActionDefineGet(id);
        }
        public async Task<RActionDefine[]> ActionDefineGetFromCache(string[] ids)
        {
            return await _roleCacheStorage.ActionDefineGet(ids);
        }
        public async Task<bool> CheckExists(string id)
        {
            return await _roleCacheStorage.CheckExists(id);
        }

        #endregion

        #region Add To Cache

        public async Task AddToCache(RActionDefine action)
        {
            await _roleCacheStorage.ActionDefineAdd(action);
        }



        #endregion

        #region Common

        #endregion



    }
}