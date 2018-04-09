using System;
using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.Config;

namespace Gico.SystemDataObject.Interfaces
{
    public interface ICategoryRepository
    {
        Task<RCategory[]> Get(string languageId);
        Task<RCategory> Get(string languageId, string id);

        Task Add(Category category);

        Task<bool> Change(Category category);
        Task<RCategoryAttr[]> GetListAttr(string id, RefSqlPaging sqlPaging);

        Task<RManufacturer[]> GetListManufacturer(string id, RefSqlPaging sqlPaging);


    }
}
