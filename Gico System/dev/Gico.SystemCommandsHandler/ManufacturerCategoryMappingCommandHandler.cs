using Gico.SystemCommands;
using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.CQRS.Model.Implements;
using Gico.SystemService.Interfaces;
using Gico.ReadSystemModels;

namespace Gico.SystemCommandsHandler
{
    public class ManufacturerCategoryMappingCommandHandler : ICommandHandler<ManufacturerCategoryMappingRemoveCommand, ICommandResult>, ICommandHandler<ManufacturerCategoryMappingAddCommand, ICommandResult>
    {
        private readonly IManufacturerCategoryMappingService _manufacturerCategoryMappingService;
        private readonly ICommonService _commonService;
        public ManufacturerCategoryMappingCommandHandler(IManufacturerCategoryMappingService manufacturerCategoryMappingService, ICommonService commonService)
        {
            _manufacturerCategoryMappingService = manufacturerCategoryMappingService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(ManufacturerCategoryMappingRemoveCommand message)
        {
            try
            {
                Manufacturer_Category_Mapping manufacturer_Category_Mapping = new Manufacturer_Category_Mapping();
                manufacturer_Category_Mapping.Remove(message);
                await _manufacturerCategoryMappingService.RemoveToDb(manufacturer_Category_Mapping);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = manufacturer_Category_Mapping.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }


        public async Task<ICommandResult> Handle(ManufacturerCategoryMappingAddCommand message)
        {
            try
            {
                Manufacturer_Category_Mapping manufacturer_Category_Mapping = new Manufacturer_Category_Mapping();
                manufacturer_Category_Mapping.Add(message);
                await _manufacturerCategoryMappingService.AddToDb(manufacturer_Category_Mapping);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = manufacturer_Category_Mapping.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

    }
}
