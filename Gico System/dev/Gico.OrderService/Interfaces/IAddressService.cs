using System.Threading.Tasks;
using Gico.ReadOrderModels;

namespace Gico.OrderService.Interfaces
{
    public interface IAddressService
    {
        #region Read

        Task<RAddress[]> GetByCustomerIdFromCache(string customerId, int top);
        Task<RAddress[]> GetByCustomerId(string customerId, int pageIndex, int pageSize);

        #endregion

        #region Write



        #endregion
    }
}