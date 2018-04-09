using Gico.Config;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemDomains.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.MarketingDataObject.Interfaces.PageBuilder
{
    public interface ITemplateRepository
    {
        Task<RTemplate> GetById(string id);        

        Task<RTemplate[]> Search(string code, string templateName, EnumDefine.CommonStatusEnum status, RefSqlPaging paging);

        Task<bool> Add(Template template);

        Task<bool> Change(Template template);
        Task ChangeStatus(string id, string userId, DateTime changeDate, EnumDefine.CommonStatusEnum status);
    }
}
