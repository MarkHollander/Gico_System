using System.Data;
using System.Threading.Tasks;
using Gico.OrderDomains;
using Gico.ReadCartModels;

namespace Gico.OrderDataObject.Interfaces
{
    public interface ICartItemDetailRepository
    {
        Task<RCartItemDetail[]> Get(string connectionString, string cartId);
        Task Add(int shardId,IDbConnection dbConnection, IDbTransaction transaction, Cart cart,
            CartItemDetail cartItemDetail);

        Task Add(int shardId, string cartId, string cartCode, IDbConnection dbConnection, IDbTransaction transaction,
            CartItemDetail cartItemDetail);

        Task Remove(string[] ids, IDbConnection dbConnection, IDbTransaction transaction);
        
    }
}