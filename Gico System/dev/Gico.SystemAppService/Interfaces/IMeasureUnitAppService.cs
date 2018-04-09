using System.Threading.Tasks;
using Gico.Models.Response;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface IMeasureUnitAppService
    {
        Task<MeasureUnitSearchResponse> Search(MeasureUnitSearchRequest request);

        Task<BaseResponse> Add(MeasureUnitAddRequest request);
        Task<BaseResponse> Change(MeasureUnitChangeRequest request);
    }
}
