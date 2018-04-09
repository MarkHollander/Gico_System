using Gico.SystemDataObject.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.ReadSystemModels.Product;

namespace Gico.SystemDataObject.Implements
{
    public class ProductRepository : SqlBaseDao, IProductRepository
    {
        #region Product

        public async Task<RProduct[]> GetById(string[] ids)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                return await connection.QueryAsync<RProduct>(ProcName.Product_GetByIds, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }

        public async Task<RProduct[]> SearchByCodeAndName(string keyword, EnumDefine.ProductStatus status, RefSqlPaging sqlPaging)
        {
            var datas = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", keyword, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.String);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.String);
                return await connection.QueryAsync<RProduct>(ProcName.Product_SearchByCodeOrName, parameters, commandType: CommandType.StoredProcedure);
            });
            return datas.ToArray();
        }
        #endregion

        #region Product_Attribute_Mapping
        #endregion

        #region Product_Manufacturer_Mapping
        #endregion

        #region Product_Category_Mapping
        #endregion

        #region Vendor_Product_Mapping
        #endregion

        #region Warehouse_Product_Mapping
        #endregion


    }
}
