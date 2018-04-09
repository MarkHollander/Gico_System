using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IRoleActionMappingRepository
    {
        Task<RRoleActionMapping[]> GetByCustomerId(string customerId);
        Task<RRoleActionMapping[]> GetByRoleId(string roleId);
        Task Change(RoleActionMapping[] roleActionMappingsAdd, string roleId, string[] actionIdsRemove);
    }
}