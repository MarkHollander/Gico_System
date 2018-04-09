using System.Threading.Tasks;
using Gico.Models.Response;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface ILocaleStringResourceAppService
    {
        Task<BaseResponse> Add(LocaleStringResourceAddRequest request);

        Task<BaseResponse> Change(LocaleStringResourceChangeRequest request);
        
        Task<LocaleStringResourceSearchResponse> Search(LocaleStringResourceSearchRequest request);

        Task<LocaleStringResourceGetResponse> Get(LocaleStringResourceGetRequest request);
    }
}
