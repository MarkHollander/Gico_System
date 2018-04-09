using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.SystemModels;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface ICustomerAppService
    {
        Task<CustomerSearchResponse> Search(CustomerSearchRequest request);
        Task<CustomerAddOrChangeResponse> AddOrChange(CustomerAddOrChangeRequest request);
        Task<CustomerGetResponse> Get( CustomerGetRequest request);
    }
}