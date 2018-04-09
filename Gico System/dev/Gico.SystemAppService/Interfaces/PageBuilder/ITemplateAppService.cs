using Gico.SystemModels.Models.PageBuilder;
using Gico.SystemModels.Request.PageBuilder;
using Gico.SystemModels.Response.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Models.Response;

namespace Gico.SystemAppService.Interfaces.PageBuilder
{
    public interface ITemplateAppService
    {
        Task<TemplateSearchResponse> Search(TemplateSearchRequest request);
        Task<TemplateGetResponse> GetTemplateById(TemplateGetRequest request);
        Task<TemplateAddOrChangeResponse> TemplateAddOrChange(TemplateAddOrChangeRequest request);
        Task<BaseResponse> TemplateRemove(TemplateRemoveRequest request);
    }
}
