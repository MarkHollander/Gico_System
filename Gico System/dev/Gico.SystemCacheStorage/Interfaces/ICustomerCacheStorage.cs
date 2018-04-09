using System.Threading.Tasks;
using Gico.ReadSystemModels;

namespace Gico.SystemCacheStorage.Interfaces
{
    public interface ICustomerCacheStorage
    {
        Task SetLoginInfo(string key, RCustomer customer);
        Task<RCustomer> GetLoginInfo(string key);
        Task<bool> CheckLoginInfoExist(string key);
    }
}