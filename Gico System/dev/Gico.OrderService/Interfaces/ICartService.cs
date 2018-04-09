using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.OrderCommands;
using Gico.OrderDomains;
using Gico.ReadCartModels;

namespace Gico.OrderService.Interfaces
{
    public interface ICartService
    {
        #region read
        Task<RCart> GetFromDb(string connectionString, string cartId);
        Task<RCart> GetCurrentCartFromDb(string connectionString, string clientId);
        Task<RCart> GetFromCache(string clientId);
        Task<RCart> GetFromCache(string clientId, EnumDefine.CartStatusEnum status);

        #endregion
        
        Task<CommandResult> Add(CartAddCommand command);
        Task<CommandResult> Change(CartItemChangeCommand command);
        Task<bool> CreatingCart(string clientId);
        Task<bool> CreatedCart(string clientId);
        Task Save(string connectionString, Cart cart, CartItem[] cartItems, CartItemDetail cartItemDetail);
        Task Save(string connectionString, int id, string cartId, string cartCode, int version, CartItem[] cartItems, CartItemDetail cartItemDetail);
        Task Remove(string connectionString, string cartId, int version, string[] cartItemIds, string[] cartItemDetailIds);
        Task<CommandResult> AddressSelected(AddressSelectedCommand command);
    }
}