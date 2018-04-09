using Gico.Config;

namespace Gico.SystemCommands
{
    public class ManufacturerCategoryMappingAddCommand: CQRS.Model.Implements.Command
    {
        public ManufacturerCategoryMappingAddCommand()
        {

        }

        public int ManufacturerId { get; set; }
        public string CategoryId { get; set; }
    }
}
