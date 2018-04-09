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
    public class VariationThemeRepository : SqlBaseDao, IVariationThemeRepository
    {
        public async Task Add(Category_VariationTheme_Mapping category_VariationTheme_Mapping)
        {
            DataTable table = new DataTable();
            table.Columns.Add("VariationThemeId", typeof(int));
            table.Columns.Add("CategoryId", typeof(string));
            foreach (var item in category_VariationTheme_Mapping.VariationThemeId)
            {
                table.Rows.Add(item, category_VariationTheme_Mapping.CategoryId);
            }

            await WithConnection(async (connection) =>
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DataMapping", table, DbType.Object);
               

                return await connection.ExecuteAsync(ProcName.Category_VariationTheme_Mapping_Insert_List, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RVariationTheme[]> Get()
        {
            var datas = await WithConnection(async (connection) =>
            {
                return await connection.QueryAsync<RVariationTheme>(ProcName.VariationTheme_Get, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }

        public async Task<RVariationTheme_Attribute[]> Get(int Id)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Id, DbType.Int16);
                return await connection.QueryAsync<RVariationTheme_Attribute>(ProcName.VariationTheme_GetAttributes, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }

        public async Task<RCategory_VariationTheme_Mapping[]> Get(string categoryId)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CategoryId", categoryId, DbType.String);
                return await connection.QueryAsync<RCategory_VariationTheme_Mapping>(ProcName.Category_VariationTheme_Mapping_Get, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }

        public async Task<RVariationTheme> GetVariationThemeById(int Id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Id, DbType.Int16);
                return await connection.QueryFirstOrDefaultAsync<RVariationTheme>(ProcName.VariationTheme_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        public async Task Remove(Category_VariationTheme_Mapping category_VariationTheme_Mapping)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@VariationThemeId", category_VariationTheme_Mapping.VariationTheme_Id, DbType.Int16);
                parameters.Add("@CategoryId", category_VariationTheme_Mapping.CategoryId, DbType.String);
                return await connection.ExecuteAsync(ProcName.Category_VariationTheme_Mapping_Remove, parameters, commandType: CommandType.StoredProcedure);
            });
        }

      

    }
}
