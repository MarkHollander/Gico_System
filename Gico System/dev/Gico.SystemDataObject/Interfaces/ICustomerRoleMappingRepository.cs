using System.Threading.Tasks;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface ICustomerRoleMappingRepository
    {
        Task AddToCustomer(CustomerRoleMapping[] customerRoleMappings,string customerId);
        Task AddToRole(CustomerRoleMapping[] customerRoleMappings, string roleId);

    }
}