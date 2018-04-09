using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class LanguageRepository : SqlBaseDao, ILanguageRepository
    {
        #region read
        public async Task<RLanguage[]> Get()
        {
            var datas = await WithConnection(async (connection) => await connection.QueryAsync<RLanguage>(ProcName.Language_Get, commandType: CommandType.StoredProcedure));
            return datas.ToArray();
        }

        public async Task<RLanguage> Get(string id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id.AsInt(), DbType.Int32);
                return await connection.QueryFirstAsync<RLanguage>(ProcName.Language_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        public async Task<RLanguage[]> Search(string name, RefSqlPaging paging)
        {
           
            
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int32);
                var data = await connection.QueryAsync<RLanguage>(ProcName.Language_Search, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }
        #endregion

        #region write

        public async Task Add(Language language)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                
                parameters.Add("@Name", language.Name, DbType.String);
                parameters.Add("@Culture", language.Culture, DbType.String);
                parameters.Add("@UniqueSeoCode", language.UniqueSeoCode, DbType.String);
                parameters.Add("@FlagImageFileName", language.FlagImageFileName, DbType.String);
                parameters.Add("@Published", language.Published, DbType.Byte);
                parameters.Add("@DisplayOrder", language.DisplayOrder, DbType.Int16);
                var data = await connection.ExecuteAsync(ProcName.Language_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<bool> Change(Language language)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", language.Id, DbType.Int16);
                parameters.Add("@Name", language.Name, DbType.String);
                parameters.Add("@Culture", language.Culture, DbType.String);
                parameters.Add("@UniqueSeoCode", language.UniqueSeoCode, DbType.String);
                parameters.Add("@FlagImageFileName", language.FlagImageFileName, DbType.String);
                parameters.Add("@Published", language.Published, DbType.Byte);
                parameters.Add("@DisplayOrder", language.DisplayOrder, DbType.Int16);
                var data = await connection.ExecuteAsync(ProcName.Language_Change, parameters, commandType: CommandType.StoredProcedure);
                return data==1;
            });
        }


        #endregion


    }
}