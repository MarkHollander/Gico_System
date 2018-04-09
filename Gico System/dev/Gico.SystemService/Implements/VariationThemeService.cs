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
    public class VariationThemeService : IVariationThemeService
    {
        private readonly IVariationThemeRepository _variationThemeRepository;
        private readonly ICommandSender _commandService;
        public VariationThemeService(IVariationThemeRepository variationThemeRepository, ICommandSender commandService)
        {
            _variationThemeRepository = variationThemeRepository;
            _commandService = commandService;

        }


        #region Command

        public async Task<CommandResult> SendCommand(Category_VariationTheme_Mapping_AddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(Category_VariationTheme_Mapping_RemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        #region Write To Db

        public async Task RemoveToDb(Category_VariationTheme_Mapping category_VariationTheme_Mapping)
        {
            await _variationThemeRepository.Remove(category_VariationTheme_Mapping);
        }
        public async Task AddToDb(Category_VariationTheme_Mapping category_VariationTheme_Mapping)
        {
            await _variationThemeRepository.Add(category_VariationTheme_Mapping);
        }


        #endregion


        public async Task<RVariationTheme[]> Get()
        {
            return await _variationThemeRepository.Get();
        }

        public async Task<RVariationTheme_Attribute[]> Get(int Id)
        {
            return await _variationThemeRepository.Get(Id);
        }

        public async Task<RCategory_VariationTheme_Mapping[]> Get(string categoryId)
        {
            return await _variationThemeRepository.Get(categoryId);
        }

        public async Task<RVariationTheme> GetVariationThemeById(int Id)
        {
            return await _variationThemeRepository.GetVariationThemeById(Id);
        }

     
    }
}
