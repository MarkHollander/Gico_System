using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels.Product;

namespace Gico.SystemService.Interfaces.Product
{
    public interface IProductService
    {
        Task<RProduct[]> GetById(string[] ids);
        Task<RProduct[]> SearchByCodeAndName(string keyword, EnumDefine.ProductStatus status,
        RefSqlPaging sqlPaging);
    }
}
