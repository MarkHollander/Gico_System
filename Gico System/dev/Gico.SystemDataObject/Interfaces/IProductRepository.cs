using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels.Product;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IProductRepository
    {
        #region Product

        Task<RProduct[]> GetById(string[] ids);
        Task<RProduct[]> SearchByCodeAndName(string keyword, EnumDefine.ProductStatus status, RefSqlPaging sqlPaging);

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
