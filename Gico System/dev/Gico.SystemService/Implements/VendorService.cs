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
    public class VendorService : IVendorService
    {
        private readonly ICommandSender _commandService;
        private readonly IVendorRepository _vendorRepository;
        private readonly ICustomerCacheStorage _customerCacheStorage;
        public VendorService(ICommandSender commandService, IVendorRepository vendorRepository, ICustomerCacheStorage customerCacheStorage)
        {
            _commandService = commandService;
            _vendorRepository = vendorRepository;
            _customerCacheStorage = customerCacheStorage;
        }

        #region READ From DB

        public async Task<RVendor[]> Search(string code, string email, string phone, string name, EnumDefine.VendorStatusEnum status, RefSqlPaging paging)
        {
            return await _vendorRepository.Search(code, email, phone, name, status, paging);
        }

        public async Task<RVendor[]> Search(string keyword, EnumDefine.VendorStatusEnum status, RefSqlPaging paging)
        {
            return await _vendorRepository.Search(keyword, status, paging);
        }

        public async Task<RVendor> GetFromDb(string id)
        {
            return await _vendorRepository.GetById(id);
        }

        public async Task<RVendor[]> GetFromDb(string[] ids)
        {
            return await _vendorRepository.GetFromDb(ids);
        }

        #endregion

        #region Write To Db

        public async Task ChangeToDb(Vendor vendor, string code)
        {
            bool isChanged = await _vendorRepository.Change(vendor, code);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Vendor_NotChanged);
            }
        }
        public async Task AddToDb(Vendor vendor)
        {
            await _vendorRepository.Add(vendor);
        }

        #endregion

        #region Command

        public async Task<CommandResult> SendCommand(VendorAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(VendorChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        #region Get From Cache

        public async Task<RCustomer> GetLoginInfoFromCache(string key)
        {
            return await _customerCacheStorage.GetLoginInfo(key);
        }

        #endregion













    }
}