using System.Threading.Tasks;
using Gico.ReadCartModels;

namespace Gico.OrderCacheStorage.Interfaces
{
    public interface ICartCacheStorage
    {
        Task<bool> CreatingCart(string clientId);
        Task<bool> CreatedCart(string clientId);
        Task<RCart> Get(string clientId);
        Task<bool> Add(RCart cart);
    }
}