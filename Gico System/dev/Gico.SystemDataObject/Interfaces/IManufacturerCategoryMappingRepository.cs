using System;
using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.Config;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IManufacturerCategoryMappingRepository
    {
        Task Remove(Manufacturer_Category_Mapping manufacturer_Category_Mapping);
        Task<RManufacturer[]> Gets(string categoryId);

        Task Add(Manufacturer_Category_Mapping manufacturer_Category_Mapping);

    }
}
