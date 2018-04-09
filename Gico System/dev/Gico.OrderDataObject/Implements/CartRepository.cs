using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.DataObject;
using Gico.Domains;
using Gico.OrderDataObject.Interfaces;
using Gico.ReadCartModels;
using Microsoft.Extensions.Primitives;
using Gico.OrderDomains;
using Gico.ExceptionDefine;

namespace Gico.OrderDataObject.Implements
{
    public class CartRepository : SqlBaseDao, ICartRepository
    {
        public async Task<RCart> Get(string connectionString, string cartId)
        {
            var data = await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", cartId, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RCart>(ProcName.ShoppingCart_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }
        public async Task<RCart> Get(string connectionString, string clientId, EnumDefine.CartStatusEnum status)
        {
            var data = await WithConnection(connectionString, async (connection) =>
             {
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@ClientId", clientId, DbType.String);
                 parameters.Add("@Status", status.AsEnumToInt(), DbType.Int64);
                 return await connection.QueryFirstOrDefaultAsync<RCart>(ProcName.ShoppingCart_GetByCustomer, parameters, commandType: CommandType.StoredProcedure);
             });
            return data;
        }
        public async Task Save(string connectionString, Cart shoppingCart,
            CartItem[] cartItems,
            CartItemDetail cartItemDetail,
            Func<int, IDbConnection, IDbTransaction, Cart, CartItem, Task> addCartItem,
            Func<int, IDbConnection, IDbTransaction, Cart, CartItemDetail, Task> addCartItemDetail)
        {
            await WithConnection(connectionString, async (connection, transaction) =>
              {
                  DynamicParameters parametersCheckExist = new DynamicParameters();
                  parametersCheckExist.Add("@ClientId", shoppingCart.ClientId, DbType.String);
                  parametersCheckExist.Add("@Status", EnumDefine.CartStatusEnum.New.AsEnumToInt(), DbType.Int64);
                  var cartExist = await connection.QueryFirstOrDefaultAsync<RCart>(ProcName.ShoppingCart_GetByCustomer, parametersCheckExist, transaction, commandType: CommandType.StoredProcedure);
                  if (cartExist != null)
                  {
                      throw new MessageException(ResourceKey.Cart_Exist);
                  }
                  DynamicParameters parameters = new DynamicParameters();
                  parameters.Add("@Id", shoppingCart.Id, DbType.String);
                  parameters.Add("@ShardId", shoppingCart.ShardId, DbType.Int32);
                  parameters.Add("@Code", shoppingCart.Code, DbType.String);
                  parameters.Add("@CreatedDateUtc", shoppingCart.CreatedDateUtc, DbType.DateTime);
                  parameters.Add("@UpdatedDateUtc", shoppingCart.CreatedDateUtc, DbType.DateTime);
                  parameters.Add("@CreatedUid", shoppingCart.CreatedUid, DbType.String);
                  parameters.Add("@UpdatedUid", shoppingCart.CreatedUid, DbType.String);
                  parameters.Add("@LanguageId", shoppingCart.LanguageId, DbType.String);
                  parameters.Add("@StoreId", shoppingCart.StoreId, DbType.String);
                  parameters.Add("@ClientId", shoppingCart.ClientId, DbType.String);
                  parameters.Add("@STATUS", shoppingCart.Status, DbType.String);
                  parameters.Add("@Version", shoppingCart.Version, DbType.String);
                  var result = await connection.ExecuteAsync(ProcName.ShoppingCart_Add, parameters, transaction, commandType: CommandType.StoredProcedure);
                  if (cartItems != null && cartItems.Length > 0)
                  {
                      foreach (var cartItem in cartItems)
                      {
                          await addCartItem(shoppingCart.ShardId, connection, transaction, shoppingCart, cartItem);
                      }
                  }
                  if (cartItemDetail != null)
                  {
                      await addCartItemDetail(shoppingCart.ShardId, connection, transaction, shoppingCart, cartItemDetail);
                  }
                  return result;
              });
        }

        public async Task<bool> Change(string id, int version, IDbConnection connection, IDbTransaction transaction)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.String);
            parameters.Add("@Version", version, DbType.Int32);
            int rowCount = await connection.ExecuteAsync(ProcName.ShoppingCart_ChangeVersion, parameters, transaction, null, CommandType.StoredProcedure);
            return rowCount > 0;
        }
    }
}