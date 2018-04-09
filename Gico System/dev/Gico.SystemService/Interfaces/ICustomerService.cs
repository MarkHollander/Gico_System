using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;

namespace Gico.SystemService.Interfaces
{
    public interface ICustomerService
    {
        #region READ From DB

        Task<RCustomer[]> Search(string code, string email, string mobile, string fullName, DateTime? fromBirthday,
            DateTime? toBirthday, EnumDefine.CustomerTypeEnum type, EnumDefine.CustomerStatusEnum status, RefSqlPaging paging);

        Task<RCustomer> GetFromDb(string id);
        Task<RCustomer> GetFromDbByEmailOrMobile(string emailOrMobile);
        Task<IEnumerable<RCustomer>> GetFromDb(string email, string mobile);
        Task<RCustomerExternalLogin[]> GetCustomerExternalLoginByCustomerId(string customerId);
        #endregion

        #region Write To Db
        Task AddToDb(Customer customer);
        Task ChangeToDb(Customer customer, string code);
        Task Change(Customer customer, CustomerExternalLogin customerExternalLogin);
        #endregion

        #region Command

        Task<CommandResult> SendCommand(CustomerRegisterCommand command);
        Task<CommandResult> SendCommand(CustomerAddCommand command);
        Task<CommandResult> SendCommand(CustomerChangeCommand command);
        Task<CommandResult> SendCommand(CustomerExternalLoginAddCommand command);

        #endregion

        #region Add To Cache

        Task SetLoginToCache(string key, RCustomer customer);

        #endregion

        #region Get From Cache

        Task<RCustomer> GetLoginInfoFromCache(string key);
        Task<bool> CheckLoginInfoFromCache(string key);

        #endregion

        #region Common
        Task<bool> ComparePassword(RCustomer customer, string loginPassword);
        #endregion




    }
}