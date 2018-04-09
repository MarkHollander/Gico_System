using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IRoleRepository
    {
        Task<RRole[]> GetByDepartmentId(string departmentId);

        Task<RRole[]> Search(string name, EnumDefine.RoleStatusEnum status, string departmentId,
            RefSqlPaging sqlPaging);
        Task<RRole> GetById(string id);
        Task Add(Role role);
        Task Change(Role role);
    }
}