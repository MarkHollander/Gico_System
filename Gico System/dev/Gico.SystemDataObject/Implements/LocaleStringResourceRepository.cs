using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class LocaleStringResourceRepository : SqlBaseDao, ILocaleStringResourceRepository
    {
        public async Task<RLocaleStringResource> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RLocaleStringResource>(ProcName.Locale_String_Resource_GetById, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RLocaleStringResource[]> GetByLanguageId(string languageId)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LanguageId", languageId, DbType.String);
                var data = await connection.QueryAsync<RLocaleStringResource>(ProcName.Locale_String_Resource_GetByLanguageId, parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task Add(LocaleStringResource locale)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LanguageId", locale.LanguageId , DbType.String);
                parameters.Add("@ResourceName", locale.ResourceName, DbType.String);
                parameters.Add("@ResourceValue", locale.ResourceValue, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Locale_String_Resource_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task Change(LocaleStringResource locale)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", locale.Id, DbType.String);
                parameters.Add("@LanguageId", locale.LanguageId, DbType.String);
                parameters.Add("@ResourceName", locale.ResourceName, DbType.String);
                parameters.Add("@ResourceValue", locale.ResourceValue, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Locale_String_Resource_Change, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RLocaleStringResource[]> Search(string languageId ,string resourceName, string resourceValue,RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LanguageId", languageId, DbType.Int32);
                parameters.Add("@ResourceName", resourceName, DbType.String);
                parameters.Add("@ResourceValue", resourceValue, DbType.String);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var data = (await connection.QueryAsync<RLocaleStringResource>(ProcName.Locale_String_Resource_Search, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    sqlPaging.TotalRow = data[0].TotalRow;
                }
                return data;
            });
        }
        
    }
}
