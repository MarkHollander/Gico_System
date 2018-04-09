using System.Threading.Tasks;
using Gico.Models.Response;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface IRoleAppService
    {
        Task<DepartmentSearchResponse> Search(DepartmentSearchRequest request);
        Task<DepartmentGetResponse> Get(DepartmentGetRequest request);
        Task<BaseResponse> Add(DepartmentAddRequest request);
        Task<BaseResponse> Change(DepartmentChangeRequest request);
        Task<RoleSearchResponse> Search(RoleSearchRequest request);
        Task<RoleGetResponse> Get(RoleGetRequest request);
        Task<BaseResponse> Add(RoleAddRequest request);
        Task<BaseResponse> Change(RoleChangeRequest request);
        Task<ActionDefineSearchResponse> Search(ActionDefineSearchRequest request);
        Task<BaseResponse> PermissionChangeByRole(PermissionChangeByRoleRequest request);
        Task<RoleActionMappingsResponse> PermissionGetAll();
    }
}