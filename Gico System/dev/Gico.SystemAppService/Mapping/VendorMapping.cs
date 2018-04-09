
using GaBon.SystemModels.Request;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Mapping
{
    public static class VendorMapping
    {
        public static VendorViewModel ToModel(this RVendor request)
        {
            if (request == null) return null;
            return new VendorViewModel()
            {
                Email = request.Email,
                Id = request.Id,
                Status = request.Status,
                Name = request.Name,
                Website=request.Website,
                Fax=request.Fax,
                CompanyName=request.CompanyName,
                Description=request.Description,
                Logo=request.Logo,
                Type=request.VendorType,
                Code = request.Code,
                Phone = request.Phone,
                CreatedDateUtc = request.CreatedDateUtc,
                CreatedUid = request.CreatedUid,
                UpdatedDateUtc = request.UpdatedDateUtc,
                UpdatedUid = request.UpdatedUid,
                Version = request.Version,
            };
        }

        public static VendorAddCommand ToCommand(this VendorAddOrChangeRequest request, string ip, string userId, string code)
        {
            if (request == null) return null;
            return new VendorAddCommand(SystemDefine.DefaultVersion)
            {          
                Phone = request.Phone,
                CompanyName = request.CompanyName,
                Name = request.Name,
                Email = request.Email,
                Description = request.Description,
                Fax = request.Fax,
                Logo = request.Logo,
                Website = request.Website,
                Code = code,
                Status = request.Status,
                Type = request.Type,
                CreatedUid = userId,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
            };
        }
        public static VendorChangeCommand ToCommand(this VendorAddOrChangeRequest request, string ip, string userId, int version)
        {
            if (request == null) return null;
            return new VendorChangeCommand(SystemDefine.DefaultVersion)
            {
                Phone = request.Phone,
                CompanyName = request.CompanyName,
                Name = request.Name,
                Email = request.Email,
                Description = request.Description,
                Fax = request.Fax,
                Logo = request.Logo,
                Website = request.Website,
                Status = request.Status,
                Type = request.Type,
                UpdatedUid = userId,
                UpdatedDateUtc = Extensions.GetCurrentDateUtc(),
                Id = request.Id,
                Version = version,
            };
        }

    }
}
