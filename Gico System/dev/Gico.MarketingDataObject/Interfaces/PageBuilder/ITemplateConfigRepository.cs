using Gico.Config;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemDomains.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.MarketingDataObject.Interfaces.PageBuilder
{
    public interface ITemplateConfigRepository
    {
        Task<RTemplateConfig> GetById(string id);

        Task<RTemplateConfig[]> Search(string id, string templateId, EnumDefine.TemplateConfigComponentTypeEnum componentType, EnumDefine.CommonStatusEnum status, RefSqlPaging paging);
        Task<RTemplateConfig[]> GetByTemplateId(string templateId);
        Task<bool> Add(TemplateConfig templateConfig);

        Task<bool> Change(TemplateConfig templateConfig);
        Task ChangeStatus(string id, string userId, DateTime changeDate, EnumDefine.CommonStatusEnum status);
    }
}
