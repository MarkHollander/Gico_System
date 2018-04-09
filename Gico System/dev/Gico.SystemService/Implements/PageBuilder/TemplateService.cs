using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.MarketingDataObject.Interfaces.PageBuilder;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemService.Interfaces.PageBuilder;
using Gico.SystemCommands.PageBuilder;
using Gico.SystemDomains.PageBuilder;
using System.Threading.Tasks;
using Gico.MarketingCacheStorage.Interfaces;

namespace Gico.SystemService.Implements.PageBuilder
{
    public class TemplateService : ITemplateService
    {
        private readonly ICommandSender _commandService;
        private readonly ITemplateRepository _templateRepository;
        private readonly ITemplateCacheStorage _templateCacheStorage;
        private readonly ITemplateConfigRepository _templateConfigRepository;

        public TemplateService(ICommandSender commandService, ITemplateRepository templateRepository, ITemplateCacheStorage templateCacheStorage, ITemplateConfigRepository templateConfigRepository)
        {
            _commandService = commandService;
            _templateRepository = templateRepository;
            _templateCacheStorage = templateCacheStorage;
            _templateConfigRepository = templateConfigRepository;
        }

        #region Db
        
        public async Task<RTemplate[]> Search(string code, string templateName, EnumDefine.CommonStatusEnum status, RefSqlPaging paging)
        {
            return await _templateRepository.Search(code, templateName, status, paging);
        }
        public async Task<RTemplate> GetById(string id)
        {
            return await _templateRepository.GetById(id);
        }
        public async Task<bool> Add(Template template)
        {
            return await _templateRepository.Add(template);
        }
        public async Task<bool> Change(Template template)
        {
            return await _templateRepository.Change(template);
        }
        public async Task ChangeTemplateStatus(string id, string userId, DateTime changeDate, EnumDefine.CommonStatusEnum status)
        {
            await _templateRepository.ChangeStatus(id, userId, changeDate, status);
        }
        public async Task<RTemplateConfig[]> SearchTemplateConfig(string id, string templateId, EnumDefine.TemplateConfigComponentTypeEnum componentType, EnumDefine.CommonStatusEnum status, RefSqlPaging paging)
        {
            return await _templateConfigRepository.Search(id, templateId, componentType, status, paging);
        }
        public async Task<RTemplateConfig[]> GetTemplateConfigByTemplateId(string templateId)
        {
            return await _templateConfigRepository.GetByTemplateId(templateId);
        }
        public async Task<RTemplateConfig> GetTemplateConfigById(string id)
        {
            return await _templateConfigRepository.GetById(id);
        }
        public async Task<bool> AddTemplateConfig(TemplateConfig templateConfig)
        {
            return await _templateConfigRepository.Add(templateConfig);
        }
        public async Task<bool> ChangeTemplateConfig(TemplateConfig templateConfig)
        {
            return await _templateConfigRepository.Change(templateConfig);
        }
        public async Task ChangeTemplateConfigStatus(string id, string userId, DateTime changeDate, EnumDefine.CommonStatusEnum status)
        {
            await _templateConfigRepository.ChangeStatus(id, userId, changeDate, status);
        }

        #endregion

        #region Command

        public async Task<CommandResult> SendCommand(TemplateConfigAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(TemplateConfigChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(TemplateAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(TemplateChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(TemplateRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(TemplateConfigRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        
        #endregion

        #region Cache

        public async Task AddTemplateToCache(RTemplateCache rTemplateCache)
        {
            await _templateCacheStorage.AddOrChange(rTemplateCache);
        }
        public async Task RemoveTemplateToCache(string id)
        {
            await _templateCacheStorage.Remove(id);
        }
        public async Task<RTemplateCache> GetTemplateToCache(string id)
        {
            return await _templateCacheStorage.Get(id);
        }
        
        #endregion
    }
}
