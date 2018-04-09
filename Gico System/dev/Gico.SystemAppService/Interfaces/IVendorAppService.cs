using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.SystemModels;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface IVendorAppService
    {
        Task<VendorSearchResponse> Search(VendorSearchRequest request);
        Task<VendorAddOrChangeResponse> AddOrChange(VendorAddOrChangeRequest request);
        Task<VendorGetResponse> Get( VendorGetRequest request);
    }
}