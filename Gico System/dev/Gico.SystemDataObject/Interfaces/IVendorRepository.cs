using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IVendorRepository
    {
        #region read 
        Task<RVendor> GetById(string id);
        Task<RVendor[]> GetFromDb(string[] ids);
        Task<RVendor[]> Search(string code, string email, string phone, string name, EnumDefine.VendorStatusEnum status, RefSqlPaging paging);
        Task<RVendor[]> Search(string keyword, EnumDefine.VendorStatusEnum status, RefSqlPaging paging);
        #endregion

        #region write
        Task Add(Vendor vendor);
        Task<bool> Change(Vendor vendor, string code);

        #endregion
    }
}