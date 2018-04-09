using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;

namespace Gico.SystemCommandsHandler
{
    public class RoleCommandHandler : ICommandHandler<ActionDefineAddCommand, ICommandResult>,
        ICommandHandler<DepartmentAddCommand, ICommandResult>,
        ICommandHandler<DepartmentChangeCommand, ICommandResult>,
        ICommandHandler<RoleAddCommand, ICommandResult>,
        ICommandHandler<RoleChangeCommand, ICommandResult>,
        ICommandHandler<RoleActionMappingChangeByRoleCommand, ICommandResult>
    {
        private readonly IRoleService _roleService;
        private readonly IEventSender _eventSender;

        public RoleCommandHandler(IRoleService roleService, IEventSender eventSender)
        {
            _roleService = roleService;
            _eventSender = eventSender;
        }

        public async Task<ICommandResult> Handle(ActionDefineAddCommand mesage)
        {
            try
            {
                ActionDefine actionDefine = new ActionDefine();
                actionDefine.Init(mesage);

                await _roleService.AddToDb(actionDefine);

                await _eventSender.Notify(actionDefine.Events);

                ICommandResult result = new CommandResult
                {
                    Message = "",
                    ObjectId = actionDefine.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(DepartmentAddCommand mesage)
        {
            try
            {
                Department department = new Department();
                department.Init(mesage);

                await _roleService.AddToDb(department);

                await _eventSender.Notify(department.Events);

                ICommandResult result = new CommandResult
                {
                    Message = "",
                    ObjectId = department.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(DepartmentChangeCommand mesage)
        {
            try
            {
                Department department = new Department();
                department.Init(mesage);

                await _roleService.ChangeToDb(department);

                await _eventSender.Notify(department.Events);

                ICommandResult result = new CommandResult
                {
                    Message = "",
                    ObjectId = department.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(RoleAddCommand mesage)
        {
            try
            {
                Role role = new Role();
                role.Init(mesage);

                await _roleService.AddToDb(role);
                await _eventSender.Notify(role.Events);

                ICommandResult result = new CommandResult
                {
                    Message = "",
                    ObjectId = role.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(RoleChangeCommand mesage)
        {
            try
            {
                Role role = new Role();
                role.Init(mesage);

                await _roleService.ChangeToDb(role);

                await _eventSender.Notify(role.Events);

                ICommandResult result = new CommandResult
                {
                    Message = "",
                    ObjectId = role.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(RoleActionMappingChangeByRoleCommand mesage)
        {
            ICommandResult result;
            try
            {
                RRole rRole = await _roleService.RoleGetByIdFromDb(mesage.RoleId);
                if (rRole == null)
                {
                    result = new CommandResult
                    {
                        Message = "Role not found.",
                        ObjectId = string.Empty,
                        Status = CommandResult.StatusEnum.Fail
                    };
                    return result;
                }
                RRoleActionMapping[] roleActionMappings =
                  await _roleService.RoleActionMappingGetByRoleIdFromDb(mesage.RoleId);

                Role role = new Role(rRole, roleActionMappings);
                role.ChangePermissionByRole(mesage, out var roleActionMappingsAdd, out var actionIdsRemove);

                await _roleService.ChangeToDb(roleActionMappingsAdd.ToArray(), mesage.RoleId, actionIdsRemove.ToArray());

                await _eventSender.Notify(role.Events);

                result = new CommandResult
                {
                    Message = "",
                    ObjectId = role.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }
    }
}