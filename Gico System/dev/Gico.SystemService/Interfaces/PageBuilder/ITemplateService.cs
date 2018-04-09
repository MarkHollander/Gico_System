using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemCommands.PageBuilder;
using Gico.SystemDomains.PageBuilder;
using Gico.SystemModels.Request.PageBuilder;
using Gico.SystemModels.Response.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.SystemService.Interfaces.PageBuilder
{
    public interface ITemplateService
    {
        #region Db
        Task<RTemplate[]> Search(string code, string templateName, EnumDefine.CommonStatusEnum templateStatus, RefSqlPaging paging);
        Task<RTemplate> GetById(string id);
        Task<bool> Add(Template template);
        Task<bool> Change(Template template);
        Task ChangeTemplateStatus(string id, string userId, DateTime changeDate, EnumDefine.CommonStatusEnum status);
        Task<RTemplateConfig[]> SearchTemplateConfig(string id, string templateId, EnumDefine.TemplateConfigComponentTypeEnum componentType, EnumDefine.CommonStatusEnum status, RefSqlPaging paging);
        Task<RTemplateConfig[]> GetTemplateConfigByTemplateId(string templateId);
        Task<RTemplateConfig> GetTemplateConfigById(string id);
        Task<bool> AddTemplateConfig(TemplateConfig templateConfig);
        Task<bool> ChangeTemplateConfig(TemplateConfig templateConfig);

        Task ChangeTemplateConfigStatus(string id, string userId, DateTime changeDate,
            EnumDefine.CommonStatusEnum status);
        #endregion

        #region Command
        Task<CommandResult> SendCommand(TemplateConfigAddCommand command);
        Task<CommandResult> SendCommand(TemplateConfigChangeCommand command);

        Task<CommandResult> SendCommand(TemplateAddCommand command);
        Task<CommandResult> SendCommand(TemplateChangeCommand command);
        Task<CommandResult> SendCommand(TemplateRemoveCommand command);
        Task<CommandResult> SendCommand(TemplateConfigRemoveCommand command);
        #endregion

        #region Cache
        Task AddTemplateToCache(RTemplateCache rTemplateCache);
        Task RemoveTemplateToCache(string id);
        Task<RTemplateCache> GetTemplateToCache(string id);
        #endregion
    }
}
