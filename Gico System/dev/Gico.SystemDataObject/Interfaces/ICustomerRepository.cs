using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface ICustomerRepository
    {
        #region read 
        Task<RCustomer> Get(string id);
        Task<RCustomer> GetByEmailOrPhone(string emailOrMobile);

        Task<RCustomer[]> Search(string code, string email, string mobile, string fullName, DateTime? fromBirthday,
            DateTime? toBirthday, EnumDefine.CustomerTypeEnum type, EnumDefine.CustomerStatusEnum status, RefSqlPaging paging);
        #endregion

        #region write
        Task Add(Customer customer);
        Task<bool> Change(Customer customer, string code);
        Task<bool> Change(Customer customer, CustomerExternalLogin customerExternalLogin);

        #endregion
    }
}