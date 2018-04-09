using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.SystemModels;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface ILanguageAppService
    {
        Task<LanguageSearchResponse> Search(LanguageSearchRequest request);
        Task<LanguageAddOrChangeResponse> AddOrChange(LanguageAddOrChangeRequest request);
    }
}