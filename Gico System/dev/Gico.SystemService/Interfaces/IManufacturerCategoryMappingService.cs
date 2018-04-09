using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.Config;

namespace Gico.SystemService.Interfaces
{
    public interface IManufacturerCategoryMappingService
    {
        Task<CommandResult> SendCommand(ManufacturerCategoryMappingRemoveCommand command);
        Task RemoveToDb(Manufacturer_Category_Mapping manufacturer_Category_Mapping);

        Task<RManufacturer[]> Gets(string categoryId);
        Task<CommandResult> SendCommand(ManufacturerCategoryMappingAddCommand command);
        Task AddToDb(Manufacturer_Category_Mapping manufacturer_Category_Mapping);


    }
}
