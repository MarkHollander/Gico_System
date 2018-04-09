using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.CQRS.Model.Implements;
using Gico.ExceptionDefine;
using Gico.Config;

namespace Gico.SystemService.Implements
{
    public class AttrCategoryService: IAttrCategoryService
    {
        private readonly IAttrCategoryRepository _attrCategoryRepository;
        private readonly ICommandSender _commandService;

        public AttrCategoryService(IAttrCategoryRepository attrCategoryRepository, ICommandSender commandService)
        {
            _attrCategoryRepository = attrCategoryRepository;
            _commandService = commandService;

        }

        #region Command
        public async Task<CommandResult> SendCommand(AttrCategoryAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(AttrCategoryChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(AttrCategoryRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        #region Write To Db

        public async Task AddToDb(AttrCategory attrCategory)
        {
            await _attrCategoryRepository.Add(attrCategory);
        }

        public async Task ChangeToDb(AttrCategory attrCategory)
        {
            bool isChanged = await _attrCategoryRepository.Change(attrCategory);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Category_NotChanged);
            }
        }

        public async Task RemoveToDb(AttrCategory attrCategory)
        {
            await _attrCategoryRepository.Remove(attrCategory);
        }

        #endregion


        #region Read from DB


        public async Task<RAttrCategory> Get(int attributeId, string categoryId)
        {
            return await _attrCategoryRepository.Get(attributeId,categoryId);
        }

        public async Task<RProductAttribute[]> GetsProductAttr(string categoryId)
        {
            return await _attrCategoryRepository.GetsProductAttr(categoryId);
        }



        #endregion
    }
}
