using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemCommands.PageBuilder;
using Gico.SystemDomains.PageBuilder;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels.PageBuilder;

namespace Gico.SystemCommandsHandler.PageBuilder
{
    public class TemplateCommandHandler : ICommandHandler<TemplateAddCommand, ICommandResult>,
        ICommandHandler<TemplateChangeCommand, ICommandResult>,
        ICommandHandler<TemplateConfigAddCommand, ICommandResult>,
        ICommandHandler<TemplateConfigChangeCommand, ICommandResult>,
        ICommandHandler<TemplateRemoveCommand, ICommandResult>,
        ICommandHandler<TemplateConfigRemoveCommand, ICommandResult>
    {
        private readonly ITemplateService _templateService;
        private readonly ICommonService _commonService;
        private readonly IEventSender _eventSender;
        public TemplateCommandHandler(ITemplateService templateService, ICommonService commonService, IEventSender eventSender)
        {
            _templateService = templateService;
            _commonService = commonService;
            _eventSender = eventSender;
        }

        public async Task<ICommandResult> Handle(TemplateAddCommand message)
        {
            try
            {
                long systemIdentity = await _commonService.GetNextId(typeof(Template));
                message.Code = Common.Common.GenerateCodeFromId(systemIdentity, 3);
                Template template = new Template();
                template.Add(message);
                await _templateService.Add(template);

                await _eventSender.Notify(template.Events);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = template.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(TemplateChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RTemplate rTemplate = await _templateService.GetById(message.Id);
                if (rTemplate == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Template not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                RTemplateConfig[] rTemplateConfigs = await _templateService.GetTemplateConfigByTemplateId(rTemplate.Id);
                Template template = new Template(rTemplate, rTemplateConfigs);
                template.Change(message);
                await _templateService.Change(template);

                await _eventSender.Notify(template.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = template.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(TemplateConfigAddCommand message)
        {
            try
            {
                ICommandResult result;
                RTemplate rTemplate = await _templateService.GetById(message.TemplateId);
                if (rTemplate == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Template not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                RTemplateConfig[] rTemplateConfigs = await _templateService.GetTemplateConfigByTemplateId(rTemplate.Id);
                Template template = new Template(rTemplate, rTemplateConfigs);
                TemplateConfig templateConfig = template.AddTemplateconfig(message);
                await _templateService.AddTemplateConfig(templateConfig);

                await _eventSender.Notify(template.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = templateConfig.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(TemplateConfigChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RTemplate rTemplate = await _templateService.GetById(message.TemplateId);
                if (rTemplate == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Template not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                RTemplateConfig[] rTemplateConfigs = await _templateService.GetTemplateConfigByTemplateId(rTemplate.Id);
                Template template = new Template(rTemplate, rTemplateConfigs);
                TemplateConfig templateConfig = template.ChangeTemplateconfig(message);
                await _templateService.ChangeTemplateConfig(templateConfig);

                await _eventSender.Notify(template.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = templateConfig.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(TemplateRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RTemplate rTemplate = await _templateService.GetById(message.Id);
                if (rTemplate == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Template not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                Template template = new Template(rTemplate);
                template.ChangeStatus(EnumDefine.CommonStatusEnum.Deleted, message.CreatedDateUtc, message.UserId);
                await _templateService.ChangeTemplateStatus(message.Id, message.UserId, message.CreatedDateUtc, EnumDefine.CommonStatusEnum.Deleted);

                await _eventSender.Notify(template.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = rTemplate.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(TemplateConfigRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RTemplate rTemplate = await _templateService.GetById(message.TemplateId);
                if (rTemplate == null)
                {
                    result = new CommandResult()
                    {
                        Message = "Template not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Template_NotFound
                    };
                    return result;
                }
                RTemplateConfig[] rTemplateConfigs = await _templateService.GetTemplateConfigByTemplateId(rTemplate.Id);
                Template template = new Template(rTemplate, rTemplateConfigs);
                TemplateConfig templateConfig = template.RemoveTemplateconfig(message);
                await _templateService.ChangeTemplateConfigStatus(templateConfig.Id, templateConfig.UpdatedUid,
                    templateConfig.UpdatedDateUtc, templateConfig.Status);

                await _eventSender.Notify(template.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = rTemplate.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        
    }
}
