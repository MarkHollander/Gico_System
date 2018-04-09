using System.Threading.Tasks;
using Gico.SystemModels.Response;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Interfaces
{
    public interface IManufacturerCategoryMappingAppService
    {
        Task<ManufacturerCategoryMappingRemoveResponse> ManufacturerCategoryMappingRemove(ManufacturerCategoryMappingRemoveRequest request);
        Task<ManufacturerMapping_GetManufacturerResponse> Gets(ManufacturerMapping_GetManufacturerRequest request);

        Task<ManufacturerMappingAddResponse> Add(ManufacturerMappingAddRequest request);
    }
}
