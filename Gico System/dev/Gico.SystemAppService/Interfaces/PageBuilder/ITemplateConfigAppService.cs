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
    public interface ITemplateConfigAppService
    {
        Task<TemplateConfigSearchResponse> Search(TemplateConfigSearchRequest request);
        Task<TemplateConfigGetResponse> GetTemplateConfigById(TemplateConfigGetRequest request);
        Task<TemplateConfigAddOrChangeResponse> TemplateConfigAddOrChange(TemplateConfigAddOrChangeRequest request);

        Task<TemplateCheckCodeExistResponse> TemplateConfigCheckCodeExist(
            TemplateCheckCodeExistRequest request);

        Task<ComponentsAutocompleteResponse> ComponentsAutocomplete(ComponentsAutocompleteRequest request);
        Task<BaseResponse> TemplateConfigRemove(TemplateConfigRemoveRequest request);
    }
}
