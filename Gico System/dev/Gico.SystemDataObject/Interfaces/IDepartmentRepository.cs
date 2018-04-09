using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<RDepartment[]> Search(string name, EnumDefine.DepartmentStatusEnum status, RefSqlPaging sqlPaging);
        Task Add(Department department);
        Task Change(Department department);
        Task<RDepartment> Get(string id);
        Task<RDepartment[]> Get(string[] ids);
    }
}