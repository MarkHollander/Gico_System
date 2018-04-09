using System.Threading.Tasks;
using Gico.ReadSystemModels;

namespace Gico.SystemDataObject.Interfaces
{
    public interface ICustomerExternalLoginRepository
    {
        Task<RCustomerExternalLogin[]> Get(string customerId);
    }
}