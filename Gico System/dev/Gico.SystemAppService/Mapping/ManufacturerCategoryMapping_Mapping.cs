using Gico.Common;
using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Mapping
{
    public static class ManufacturerCategoryMapping_Mapping
    {
        public static ManufacturerCategoryMappingRemoveCommand ToRemoveCommand(this ManufacturerCategoryMappingModel manufacturerCategoryMapping)
        {
            if (manufacturerCategoryMapping == null) return null;
            return new ManufacturerCategoryMappingRemoveCommand()
            {
                ManufacturerId = manufacturerCategoryMapping.ManufacturerId,
                CategoryId = manufacturerCategoryMapping.CategoryId

            };
        }

        public static ManufacturerCategoryMappingAddCommand ToAddCommand(this ManufacturerMappingAddRequest manufacturer)
        {
            if (manufacturer == null) return null;
            return new ManufacturerCategoryMappingAddCommand()
            {
               CategoryId = manufacturer.CategoryId,
               ManufacturerId = manufacturer.ManufacturerId


            };
        }


    }
}
