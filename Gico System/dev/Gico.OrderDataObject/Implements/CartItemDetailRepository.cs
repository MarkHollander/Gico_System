using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderDomains;
using Gico.ReadCartModels;

namespace Gico.OrderDataObject.Implements
{
    public class CartItemDetailRepository : SqlBaseDao, ICartItemDetailRepository
    {
        public async Task<RCartItemDetail[]> Get(string connectionString, string cartId)
        {
            return await WithConnection(connectionString, async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CartId", cartId, DbType.String);
                var datas = await connection.QueryAsync<RCartItemDetail>(ProcName.ShoppingCartItemDetail_GetByCartId, parameters, null, null, CommandType.StoredProcedure);
                return datas.ToArray();
            });
        }

        public async Task Add(int shardId, IDbConnection dbConnection, IDbTransaction transaction, Cart cart, CartItemDetail cartItemDetail)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ShoppingCartId", cart.Id, DbType.String);
            parameters.Add("@ShoppingCartCode", cart.Code, DbType.String);
            parameters.Add("@Id", cartItemDetail.Id, DbType.String);
            parameters.Add("@ShardId", shardId, DbType.Int32);
            parameters.Add("@CreatedDateUtc", cart.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@UpdatedDateUtc", cart.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@CreatedUid", cart.CreatedUid, DbType.String);
            parameters.Add("@UpdatedUid", cart.CreatedUid, DbType.String);
            parameters.Add("@LanguageId", cart.LanguageId, DbType.String);
            parameters.Add("@ProductId", cartItemDetail.ProductId, DbType.String);
            parameters.Add("@NAME", cartItemDetail.Name, DbType.String);
            await dbConnection.ExecuteAsync(ProcName.ShoppingCartItemDetail_Add, parameters, transaction, null,
                CommandType.StoredProcedure);
        }
        public async Task Add(int shardId, string cartId, string cartCode, IDbConnection dbConnection, IDbTransaction transaction, CartItemDetail cartItemDetail)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ShoppingCartId", cartId, DbType.String);
            parameters.Add("@ShoppingCartCode", cartCode, DbType.String);
            parameters.Add("@Id", cartItemDetail.Id, DbType.String);
            parameters.Add("@ShardId", shardId, DbType.Int32);
            parameters.Add("@CreatedDateUtc", cartItemDetail.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@UpdatedDateUtc", cartItemDetail.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@CreatedUid", cartItemDetail.CreatedUid, DbType.String);
            parameters.Add("@UpdatedUid", cartItemDetail.CreatedUid, DbType.String);
            parameters.Add("@LanguageId", cartItemDetail.LanguageId, DbType.String);
            parameters.Add("@ProductId", cartItemDetail.ProductId, DbType.String);
            parameters.Add("@NAME", cartItemDetail.Name, DbType.String);
            await dbConnection.ExecuteAsync(ProcName.ShoppingCartItemDetail_Add, parameters, transaction, null,
                CommandType.StoredProcedure);
        }

        public async Task Remove(string[] ids, IDbConnection dbConnection, IDbTransaction transaction)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Ids", string.Join(",", ids), DbType.String);
            await dbConnection.ExecuteAsync(ProcName.ShoppingCartItemDetail_Remove, parameters, transaction, null,
                CommandType.StoredProcedure);
        }

    }
}