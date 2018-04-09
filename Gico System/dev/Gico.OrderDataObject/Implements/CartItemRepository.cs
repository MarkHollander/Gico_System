using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.Domains;
using Gico.ExceptionDefine;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderDomains;
using Gico.ReadCartModels;

namespace Gico.OrderDataObject.Implements
{
    public class CartItemRepository : SqlBaseDao, ICartItemRepository
    {
        #region Read

        public async Task<RCartItem[]> Get(string connectionString, string cartId, EnumDefine.CartStatusEnum status)
        {
            return await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CartId", cartId, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int64);
                parameters.Add("@AllStatus", EnumDefine.CartStatusEnum.AllStatus.AsEnumToInt(), DbType.Int64);
                var datas = await connection.QueryAsync<RCartItem>(ProcName.ShoppingCartItem_GetByCartId, parameters, null, null, CommandType.StoredProcedure);
                return datas.ToArray();
            });
        }

        #endregion

        #region Write

        public async Task Add(int shardId, IDbConnection dbConnection, IDbTransaction transaction, Cart cart, CartItem cartItem)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ShoppingCartId", cart.Id, DbType.String);
            parameters.Add("@ShoppingCartCode", cart.Code, DbType.String);
            parameters.Add("@Id", cartItem.Id, DbType.String);
            parameters.Add("@ShardId", shardId, DbType.Int32);
            parameters.Add("@CreatedDateUtc", cart.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@UpdatedDateUtc", cart.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@CreatedUid", cart.CreatedUid, DbType.String);
            parameters.Add("@UpdatedUid", cart.CreatedUid, DbType.String);
            parameters.Add("@LanguageId", cart.LanguageId, DbType.String);
            parameters.Add("@StoreId", cart.StoreId, DbType.String);
            parameters.Add("@STATUS", cartItem.Status, DbType.Int64);
            parameters.Add("@ProductId", cartItem.ProductId, DbType.String);
            parameters.Add("@Price", cartItem.Price, DbType.Decimal);
            await dbConnection.ExecuteAsync(ProcName.ShoppingCartItem_Add, parameters, transaction, null,
                CommandType.StoredProcedure);
        }
        public async Task Add(string connectionString, int shardId, string cartId, string cartCode, int version, CartItem[] cartItems, CartItemDetail cartItemDetail,
            Func<int, string, string, IDbConnection, IDbTransaction, CartItemDetail, Task> addCartItemDetail,
            Func<string, int, IDbConnection, IDbTransaction, Task<bool>> changeCart)
        {
            await WithConnection(connectionString, async (connection, transaction) =>
             {
                 bool isChangeCart = await changeCart(cartId, version, connection, transaction);
                 if (!isChangeCart)
                 {
                     throw new MessageException(ResourceKey.Cart_IsChanged);
                 }
                 foreach (var shoppingCartItem in cartItems)
                 {
                     DynamicParameters parameters = new DynamicParameters();
                     parameters.Add("@ShoppingCartId", cartId, DbType.String);
                     parameters.Add("@ShoppingCartCode", cartCode, DbType.String);
                     parameters.Add("@Id", shoppingCartItem.Id, DbType.String);
                     parameters.Add("@ShardId", shardId, DbType.Int32);
                     parameters.Add("@CreatedDateUtc", shoppingCartItem.CreatedDateUtc, DbType.DateTime);
                     parameters.Add("@UpdatedDateUtc", shoppingCartItem.CreatedDateUtc, DbType.DateTime);
                     parameters.Add("@CreatedUid", shoppingCartItem.CreatedUid, DbType.String);
                     parameters.Add("@UpdatedUid", shoppingCartItem.CreatedUid, DbType.String);
                     parameters.Add("@LanguageId", shoppingCartItem.LanguageId, DbType.String);
                     parameters.Add("@StoreId", shoppingCartItem.StoreId, DbType.String);
                     parameters.Add("@STATUS", shoppingCartItem.Status, DbType.Int64);
                     parameters.Add("@ProductId", shoppingCartItem.ProductId, DbType.String);
                     parameters.Add("@Price", shoppingCartItem.Price, DbType.Decimal);
                     await connection.ExecuteAsync(ProcName.ShoppingCartItem_Add, parameters, transaction, null,
                         CommandType.StoredProcedure);
                 }
                 if (cartItemDetail != null)
                 {
                     await addCartItemDetail(shardId, cartId, cartCode, connection, transaction, cartItemDetail);
                 }
                 return Task.FromResult(true);
             });
        }
        public async Task Remove(string connectionString, string cartId, int version, string[] cartItemIds, string[] cartItemDetailIds,
            Func<string, int, IDbConnection, IDbTransaction, Task<bool>> changeCart,
            Func<string[], IDbConnection, IDbTransaction, Task> removeCartItemDetail)
        {
            await WithConnection(connectionString, async (connection, transaction) =>
            {
                bool isChangeCart = await changeCart(cartId, version, connection, transaction);
                if (!isChangeCart)
                {
                    throw new MessageException(ResourceKey.Cart_IsChanged);
                }
                if (cartItemIds != null && cartItemIds.Length > 0)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Ids", string.Join(",", cartItemIds), DbType.String);
                    await connection.ExecuteAsync(ProcName.ShoppingCartItem_Remove, parameters, transaction, null, CommandType.StoredProcedure);
                }
                if (cartItemDetailIds != null && cartItemDetailIds.Length > 0)
                {
                    await removeCartItemDetail(cartItemDetailIds, connection, transaction);
                }
                return true;
            });
        }
        public async Task RemoveWithCountItem(string connectionString, string cartId, string cartCode, int version, CartItem cartItem, int quantity,
            Func<string, int, IDbConnection, IDbTransaction, Task<bool>> changeCart)
        {
            await WithConnection(connectionString, async (connection, transaction) =>
             {
                 bool isChangeCart = await changeCart(cartId, version, connection, transaction);
                 if (!isChangeCart)
                 {
                     throw new MessageException(ResourceKey.Cart_IsChanged);
                 }
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@ShoppingCartId", cartId, DbType.String);
                 parameters.Add("@ProductId", cartItem.ProductId, DbType.String);
                 parameters.Add("@Price", cartItem.Price, DbType.Decimal);
                 parameters.Add("@Status", (int)EnumDefine.CartStatusEnum.Remove, DbType.Int64);
                 parameters.Add("@Top", quantity * -1, DbType.Int32);
                 return await connection.ExecuteAsync(ProcName.ShoppingCartItem_ChangeStatusWithCountItem, parameters, transaction, null,
                     CommandType.StoredProcedure);
             });
        }

        #endregion
    }
}