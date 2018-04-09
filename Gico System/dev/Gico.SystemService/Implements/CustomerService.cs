using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.ExceptionDefine;
using Gico.ReadSystemModels;
using Gico.SystemService.Interfaces;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemCacheStorage.Interfaces;

namespace Gico.SystemService.Implements
{
    public class CustomerService : ICustomerService
    {
        private readonly ICommandSender _commandService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerCacheStorage _customerCacheStorage;
        private readonly ICustomerExternalLoginRepository _customerExternalLoginRepository;
        public CustomerService(ICommandSender commandService, ICustomerRepository customerRepository, ICustomerCacheStorage customerCacheStorage, ICustomerExternalLoginRepository customerExternalLoginRepository)
        {
            _commandService = commandService;
            _customerRepository = customerRepository;
            _customerCacheStorage = customerCacheStorage;
            _customerExternalLoginRepository = customerExternalLoginRepository;
        }

        #region READ From DB

        public async Task<RCustomer[]> Search(string code, string email, string mobile, string fullName, DateTime? fromBirthday,
            DateTime? toBirthday, EnumDefine.CustomerTypeEnum type, EnumDefine.CustomerStatusEnum status, RefSqlPaging paging)
        {
            return await _customerRepository.Search(code, email, mobile, fullName, fromBirthday, toBirthday, type, status, paging);
        }

        public async Task<RCustomer> GetFromDb(string id)
        {
            return await _customerRepository.Get(id);
        }

        public async Task<RCustomer> GetFromDbByEmailOrMobile(string emailOrMobile)
        {
            var customer = await _customerRepository.GetByEmailOrPhone(emailOrMobile);
            if (customer != null)
                customer.CustomerExternalLogins = await GetCustomerExternalLoginByCustomerId(customer.Id);
            return customer;
        }

        public async Task<IEnumerable<RCustomer>> GetFromDb(string email, string mobile)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RCustomerExternalLogin[]> GetCustomerExternalLoginByCustomerId(string customerId)
        {
            return await _customerExternalLoginRepository.Get(customerId);
        }
        #endregion

        #region Write To Db

        public async Task ChangeToDb(Customer customer, string code)
        {
            bool isChanged = await _customerRepository.Change(customer, code);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Customer_NotChanged);
            }
        }

        public async Task Change(Customer customer, CustomerExternalLogin customerExternalLogin)
        {
            bool isChanged = await _customerRepository.Change(customer, customerExternalLogin);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Customer_Version_Changed);
            }
        }

        public async Task AddToDb(Customer customer)
        {
            await _customerRepository.Add(customer);
        }

        #endregion

        #region Command

        public async Task<CommandResult> SendCommand(CustomerRegisterCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(CustomerAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(CustomerChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(CustomerExternalLoginAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        #endregion

        #region Add To Cache

        public async Task SetLoginToCache(string key, RCustomer customer)
        {
            await _customerCacheStorage.SetLoginInfo(key, customer);
        }

        #endregion

        #region Get From Cache

        public async Task<RCustomer> GetLoginInfoFromCache(string key)
        {
            return await _customerCacheStorage.GetLoginInfo(key);
        }

        public async Task<bool> CheckLoginInfoFromCache(string key)
        {
            return await _customerCacheStorage.CheckLoginInfoExist(key);
        }

        #endregion

        #region Common

        public async Task<bool> ComparePassword(RCustomer customer, string loginPassword)
        {
            string passwordHash = EncryptionExtensions.Encryption(customer.Code, loginPassword, customer.PasswordSalt);
            return await Task.FromResult(customer.Password.Equals(passwordHash));
        }

        #endregion











    }
}