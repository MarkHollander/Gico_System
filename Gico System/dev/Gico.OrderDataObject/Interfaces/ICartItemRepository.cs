using System;
using System.Data;
using System.Threading.Tasks;
using Gico.Config;
using Gico.OrderDomains;
using Gico.ReadCartModels;

namespace Gico.OrderDataObject.Interfaces
{
    public interface ICartItemRepository
    {
        #region READ

        Task<RCartItem[]> Get(string connectionString, string cartId, EnumDefine.CartStatusEnum status);


        #endregion
        Task Add(int shardId, IDbConnection dbConnection, IDbTransaction transaction, Cart cart, CartItem cartItem);

        Task Add(string connectionString, int shardId, string cartId, string cartCode, int version, CartItem[] cartItems,
            CartItemDetail cartItemDetail,
            Func<int, string, string, IDbConnection, IDbTransaction, CartItemDetail, Task> addCartItemDetail,
            Func<string, int, IDbConnection, IDbTransaction, Task<bool>> changeCart);

        Task Remove(string connectionString, string cartId, int version, string[] cartItemIds, string[] cartItemDetailIds,
            Func<string, int, IDbConnection, IDbTransaction, Task<bool>> changeCart,
            Func<string[], IDbConnection, IDbTransaction, Task> removeCartItemDetail);
        Task RemoveWithCountItem(string connectionString, string cartId, string cartCode, int version, CartItem cartItem, int quantity,
            Func<string, int, IDbConnection, IDbTransaction, Task<bool>> changeCart);
    }
}