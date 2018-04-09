using Gico.Caching.Redis;
using Gico.CQRS.Service.Interfaces;
using Gico.MarketingDataObject.Interfaces.ProductGroup;
using Gico.SystemService.Interfaces.ProductGroup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.ReadSystemModels.ProductGroup;
using Gico.SystemCommands.ProductGroup;
using Gico.SystemDomains.ProductGroup;

namespace Gico.SystemService.Implements.ProductGroup
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly ICommandSender _commandService;
        private readonly IProductGroupRepository _productGroupRepository;

        public ProductGroupService(ICommandSender commandService, IProductGroupRepository productGroupRepository)
        {
            this._commandService = commandService;
            this._productGroupRepository = productGroupRepository;
        }

        #region Read FromDB

        public async Task<RProductGroup[]> Search(string name, EnumDefine.CommonStatusEnum status, RefSqlPaging sqlPaging)
        {
            return await _productGroupRepository.Search(name, status, sqlPaging);
        }

        public async Task<RProductGroup> Get(string id)
        {
            return await _productGroupRepository.Get(id);
        }

        #endregion

        #region Change To DB

        public async Task Add(SystemDomains.ProductGroup.ProductGroup productGroup)
        {
            await _productGroupRepository.Add(productGroup);
        }

        public async Task Change(SystemDomains.ProductGroup.ProductGroup productGroup)
        {
            await _productGroupRepository.Change(productGroup);
        }

        public async Task ChangeConditions(string id, IDictionary<EnumDefine.ProductGroupConfigTypeEnum, ProductGroupCondition> conditions, string updatedUid, DateTime updatedDate)
        {
            string json = Common.Serialize.JsonSerializeObject(conditions);
            await _productGroupRepository.ChangeConditions(id, json, updatedUid, updatedDate);
        }


        #endregion

        #region Send Command

        public async Task<CommandResult> SendCommand(ProductGroupAddCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        public async Task<CommandResult> SendCommand(ProductGroupChangeCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        public async Task<CommandResult> SendCommand(ProductGroupCategoryChangeCommand command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        public async Task<CommandResult> SendCommand(Command command)
        {
            return await _commandService.SendAndReceiveResult<CommandResult>(command);
        }

        #endregion

    }
}
