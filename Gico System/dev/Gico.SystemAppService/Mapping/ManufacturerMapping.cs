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
    public static class ManufacturerMapping
    {
        public static ManufacturerViewModel ToModel(this RManufacturer manufacturer)
        {
            if (manufacturer == null) return null;
            return new ManufacturerViewModel()
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Description = manufacturer.Description,
                Logo = manufacturer.Logo,
                Status = manufacturer.Status
            };
        }

        public static ManufacturerManagementAddCommand ToCommand_Add(this ManufacturerManagementAddOrChangeRequest request)
        {
            if (request == null) return null;
            else return new ManufacturerManagementAddCommand(SystemDefine.DefaultVersion)
            {
                Name = request.Name,
                Description = request.Description,
                Logo = request.Logo,               
                CreatedDateUtc = Extensions.GetCurrentDateUtc()
            };
        }

        public static ManufacturerManagementChangeCommand ToCommand_Change(this ManufacturerManagementAddOrChangeRequest request)
        {
            if (request == null) return null;
            else return new ManufacturerManagementChangeCommand(SystemDefine.DefaultVersion)
            {
                Id= request.Id,
                Name = request.Name,
                Description = request.Description,
                Logo = request.Logo,                
                UpdatedDateUtc = Extensions.GetCurrentDateUtc()              
            };
        }
        

    }
}
