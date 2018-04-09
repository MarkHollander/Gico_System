using System.Threading.Tasks;
using Gico.OrderService.Interfaces;
using Gico.ReadOrderModels;

namespace Gico.OrderService.Implements
{
    public class AddressService: IAddressService
    {
        public async Task<RAddress[]> GetByCustomerIdFromCache(string customerId, int top)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RAddress[]> GetByCustomerId(string customerId, int pageIndex, int pageSize)
        {
            throw new System.NotImplementedException();
        }
    }
}