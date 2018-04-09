using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.ExceptionDefine;
using Gico.ReadSystemModels;
using Gico.SystemService.Interfaces;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemService.Implements
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly ICommandSender _commandService;
        private readonly IProductAttributeRepository _repository;
        public ProductAttributeService(ICommandSender commandService, IProductAttributeRepository productAttributeRepository)
        {
            _commandService = commandService;
            _repository = productAttributeRepository;
        }

        #region Get

        public async Task<RProductAttribute[]> Search(string attributeId, string attributeName, RefSqlPaging paging)
        {
            return await _repository.Search(attributeId, attributeName, paging);
        }

        public async Task<RProductAttribute> GetFromDb(string attributeId)
        {
            return await _repository.Get(attributeId);
        }

        public async Task<RProductAttribute[]> GetFromDb(string[] attributeIds)
        {
            return await _repository.Get(attributeIds);
        }

        #endregion

        #region Command

        public async Task<CommandResult> SendCommand(ProductAttributeCommand command)
        {
            var commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        #region CRUD

        public async Task AddToDb(ProductAttribute productAttribute)
        {
            await _repository.Add(productAttribute);
        }

        public async Task UpdateToDb(ProductAttribute productAttribute)
        {
            var result = await _repository.Update(productAttribute);
            if (!result)
            {
                throw new MessageException(ResourceKey.Customer_NotChanged);
            }
        }

        #endregion
    }

    public class ProductAttributeValueService : IProductAttributeValueService
    {
        private readonly ICommandSender _commandService;
        private readonly IProductAttributeValueRepository _repository;
        public ProductAttributeValueService(ICommandSender commandService, IProductAttributeValueRepository productAttributeValueRepository)
        {
            _commandService = commandService;
            _repository = productAttributeValueRepository;
        }

        #region Get

        public async Task<RProductAttributeValue[]> Search(string attributeId, string attributeValueId, string value, RefSqlPaging paging)
        {
            return await _repository.Search(attributeId, attributeValueId, value, paging);
        }

        public async Task<RProductAttributeValue> GetFromDb(string attributeValueId)
        {
            return await _repository.Get(attributeValueId);
        }

        public async Task<RProductAttributeValue[]> GetFromDb(string[] attributeValueIds)
        {
            return await _repository.Get(attributeValueIds);
        }

        #endregion

        #region Command

        public async Task<CommandResult> SendCommand(ProductAttributeValueCommand command)
        {
            var commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        #region CRUD

        public async Task AddToDb(ProductAttributeValue productAttributeValue)
        {
            await _repository.Add(productAttributeValue);
        }

        public async Task UpdateToDb(ProductAttributeValue productAttributeValue)
        {
            var result = await _repository.Update(productAttributeValue);
            if (!result)
            {
                throw new MessageException(ResourceKey.Customer_NotChanged);
            }
        }

        #endregion
    }
}