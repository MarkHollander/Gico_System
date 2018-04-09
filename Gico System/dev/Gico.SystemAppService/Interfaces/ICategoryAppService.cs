using System.Threading.Tasks;
using Gico.SystemModels.Response;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Interfaces
{
    public interface ICategoryAppService
    {
        Task<CategoryGetsResponse> CategoryGet(CategoryGetsRequest request);
        Task<CategoryGetResponse> CategoryGet(CategoryGetRequest request);
        Task<CategoryAddOrChangeResponse> CategoryAddOrChange(CategoryAddOrChangeRequest request);
        Task<CategoryAttrResponse> GetListAttr(CategoryAttrRequest request);
        Task<CategoryManufacturerGetListResponse> GetListManufacturer(CategoryManufacturerGetListRequest request);
    }
}
