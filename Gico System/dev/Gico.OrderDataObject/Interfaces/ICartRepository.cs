using System;
using System.Data;
using System.Threading.Tasks;
using Gico.Config;
using Gico.OrderDomains;
using Gico.ReadCartModels;

namespace Gico.OrderDataObject.Interfaces
{
    public interface ICartRepository
    {
        Task<RCart> Get(string connectionString, string cartId);
        Task<RCart> Get(string connectionString, string clientId, EnumDefine.CartStatusEnum status);

        Task Save(string connectionString, Cart shoppingCart,
            CartItem[] cartItem,
            CartItemDetail cartItemDetail,
            Func<int, IDbConnection, IDbTransaction, Cart, CartItem, Task> addCartItem,
            Func<int, IDbConnection, IDbTransaction, Cart, CartItemDetail, Task> addCartItemDetail);

        Task<bool> Change(string id, int version, IDbConnection connection, IDbTransaction transaction);
    }
}