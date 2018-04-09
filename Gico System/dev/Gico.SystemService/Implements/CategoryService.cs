using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemService.Interfaces;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.CQRS.Model.Implements;
using Gico.ExceptionDefine;
using Gico.Config;

namespace Gico.SystemService.Implements
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommandSender _commandService;

        public CategoryService(ICategoryRepository categoryRepository, ICommandSender commandService)
        {
            _categoryRepository = categoryRepository;
            _commandService = commandService;

        }
        #region Read from DB
        public async Task<RCategory[]> Get(string languageId)
        {
            return await _categoryRepository.Get(languageId);
        }

        public async Task<RCategory> Get(string languageId, string id)
        {
            return await _categoryRepository.Get(languageId, id);
        }

        public async Task<RCategoryAttr[]> GetListAttr(string id, RefSqlPaging sqlPaging)
        {
            return await _categoryRepository.GetListAttr(id, sqlPaging);
        }

        #endregion

        #region Command
        public async Task<CommandResult> SendCommand(CategoryAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(CategoryChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion


        #region Write To Db

        public async Task AddToDb(Category category)
        {
            await _categoryRepository.Add(category);
        }


        public async Task ChangeToDb(Category category)
        {
            bool isChanged = await _categoryRepository.Change(category);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Category_NotChanged);
            }
        }

        public async Task<RManufacturer[]> GetListManufacturer(string id, RefSqlPaging sqlPaging)
        {
            return await _categoryRepository.GetListManufacturer(id, sqlPaging);
        }

        #endregion





    }
}
