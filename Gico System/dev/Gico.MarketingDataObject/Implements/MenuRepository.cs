using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.MarketingDataObject.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.MarketingDataObject.Implements
{
    public class MenuRepository : SqlBaseDao, IMenuRepository
    {
        public async Task<RMenu[]> GetByLanguageId(string languageId)
        {
            var datas = await WithConnection(async (connection) =>
              {
                  DynamicParameters parameters = new DynamicParameters();
                  parameters.Add("@LanguageId", languageId, DbType.String);
                  return await connection.QueryAsync<RMenu>(ProcName.Menu_GetByLanguageId, parameters, commandType: CommandType.StoredProcedure);
              });
            return datas.ToArray();
        }

        public async Task<RMenu> Get(string id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RMenu>(ProcName.Menu_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        public async Task Add(Menu menu)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", menu.Id, DbType.String);
                parameters.Add("@ParentId", menu.ParentId, DbType.String);
                parameters.Add("@NAME", menu.Name, DbType.String);
                parameters.Add("@TYPE", menu.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@ObjectId", string.Empty, DbType.String);
                parameters.Add("@URL", menu.Url, DbType.String);
                parameters.Add("@Condition", menu.Condition, DbType.String);
                parameters.Add("@POSITION", menu.Position, DbType.Int64);
                parameters.Add("@CreatedDateUtc", menu.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", menu.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", menu.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", menu.CreatedUid, DbType.String);
                parameters.Add("@LanguageId", menu.LanguageId, DbType.String);
                parameters.Add("@STATUS", menu.Status, DbType.Int64);
                parameters.Add("@StoreId", menu.StoreId, DbType.String);
                parameters.Add("@Priority", menu.Priority, DbType.Int32);
                return await connection.ExecuteAsync(ProcName.Menu_Add, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task Change(Menu menu)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", menu.Id, DbType.String);
                parameters.Add("@ParentId", menu.ParentId, DbType.String);
                parameters.Add("@NAME", menu.Name, DbType.String);
                parameters.Add("@TYPE", menu.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@ObjectId", string.Empty, DbType.String);
                parameters.Add("@URL", menu.Url, DbType.String);
                parameters.Add("@Condition", menu.Condition, DbType.String);
                parameters.Add("@POSITION", menu.Position, DbType.Int64);
                parameters.Add("@UpdatedDateUtc", menu.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", menu.UpdatedUid, DbType.String);
                parameters.Add("@LanguageId", menu.LanguageId, DbType.String);
                parameters.Add("@STATUS", menu.Status, DbType.Int64);
                parameters.Add("@StoreId", menu.StoreId, DbType.String);
                parameters.Add("@Priority", menu.Priority, DbType.Int32);
                return await connection.ExecuteAsync(ProcName.Menu_Change, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task Remove(Menu menu)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", menu.Id, DbType.String);
                return await connection.ExecuteAsync(ProcName.Menu_RemoveById, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task AddOrChangeMenuBannerMapping(DataTable dataTable, string menuId)
        {
            await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuId", menuId, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Menu_Banner_Mapping_RemoveByBannerId, parameters, transaction, commandType: CommandType.StoredProcedure);
                await BulkCopy(dataTable, connection, transaction);
                return data;
            });
        }

        public async Task AddMenuBannerMapping(string menuId, string bannerId)
        {
            await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuId", menuId, DbType.String);
                parameters.Add("@BannerId", bannerId, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Menu_Banner_Mapping_Add, parameters, transaction, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task RemoveMenuBannerMapping(string menuId, string bannerId)
        {
            await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuId", menuId, DbType.String);
                parameters.Add("@BannerId", bannerId, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Menu_Banner_Mapping_Remove, parameters, transaction, commandType: CommandType.StoredProcedure);
                return data;
            });
        }
    }
}
