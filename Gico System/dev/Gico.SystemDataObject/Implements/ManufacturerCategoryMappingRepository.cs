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
    public class ManufacturerCategoryMappingRepository : SqlBaseDao, IManufacturerCategoryMappingRepository
    {
        public async Task Remove(Manufacturer_Category_Mapping manufacturer_Category_Mapping)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ManufacturerId", manufacturer_Category_Mapping.ManufacturerId, DbType.Int16);
                parameters.Add("@CategoryId", manufacturer_Category_Mapping.CategoryId, DbType.String);
                return await connection.ExecuteAsync(ProcName.ManufacturerCategoryMapping_Delete, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RManufacturer[]> Gets(string categoryId)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id",categoryId, DbType.String);
                return await connection.QueryAsync<RManufacturer>(ProcName.ManufacturerCategoryMapping_GetListManufacturer, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }


        public async Task Add(Manufacturer_Category_Mapping manufacturer_Category_Mapping)
        {
            await WithConnection(async (connection) =>
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IdManufacturer", manufacturer_Category_Mapping.ManufacturerId, DbType.Int16);
                parameters.Add("@CategoryId", manufacturer_Category_Mapping.CategoryId, DbType.String);   

                return await connection.ExecuteAsync(ProcName.ManufacturerCategoryMapping_Add, parameters, commandType: CommandType.StoredProcedure);
            });
        }

    }
}
