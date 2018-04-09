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
    public class ManufacturerCategoryMappingService : IManufacturerCategoryMappingService
    {
        private readonly IManufacturerCategoryMappingRepository _manufacturerCategoryMappingRepository;
        private readonly ICommandSender _commandService;

        public ManufacturerCategoryMappingService(IManufacturerCategoryMappingRepository manufacturerCategoryMappingRepository, ICommandSender commandService)
        {
            _manufacturerCategoryMappingRepository = manufacturerCategoryMappingRepository;
            _commandService = commandService;

        }
        public async Task<CommandResult> SendCommand(ManufacturerCategoryMappingAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }


    

        public async Task<RManufacturer[]> Gets(string categoryId)
        {
            return await _manufacturerCategoryMappingRepository.Gets(categoryId);
        }




        #region Write To Db




        public async Task RemoveToDb(Manufacturer_Category_Mapping manufacturer_Category_Mapping)
        {
            await _manufacturerCategoryMappingRepository.Remove(manufacturer_Category_Mapping);
        }

        #endregion


        public async Task<CommandResult> SendCommand(ManufacturerCategoryMappingRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

      
        public async Task AddToDb(Manufacturer_Category_Mapping manufacturer_Category_Mapping)
        {
            await _manufacturerCategoryMappingRepository.Add(manufacturer_Category_Mapping);
        }
    }
}
