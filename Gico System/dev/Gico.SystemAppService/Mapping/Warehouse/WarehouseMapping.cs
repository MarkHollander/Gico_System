using GaBon.SystemModels.Request;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels.Product;
using Gico.ReadSystemModels.Warehouse;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;
using Gico.SystemModels.Models.Warehouse;
using Gico.SystemModels.Request;
using Gico.SystemService.Interfaces;

namespace Gico.SystemAppService.Mapping.Warehouse
{
    public static class WarehouseMapping
    {
        public static WarehouseViewModel ToModel(this RWarehouse request, string provinceName, string districtName, string villageName, string roadName)
        {
            if (request == null) return null;
            return new WarehouseViewModel
            {
                AddressDetail = request.AddressDetail,
                Code = request.Code,
                Name = request.Name,
                DistrictId = request.DistrictId,
                DistrictName = districtName,
                Email = request.Email,
                Id = request.Id,
                PhoneNumber = request.PhoneNumber,
                ProvinceId = request.ProvinceId,
                ProvinceName = provinceName,
                RoadId = request.RoadId,
                RoadName = roadName,
                Status = request.Status,
                VillageId = request.VillageId,
                VillageName = villageName,
                VendorId = request.VendorId,
                Description = request.Description,
                Type = request.Type,


            };
        }
        public static WarehouseViewModel ToModel(this RWarehouse request, string provinceName, string districtName, string villageName, string roadName, string venderName)
        {
            if (request == null) return null;
            var warehouse = request.ToModel(provinceName, districtName, villageName, roadName);
            if (warehouse != null)
            {
                warehouse.VendorName = venderName;
            }
            return warehouse;
        }
        public static WarehouseViewModel ToModel(this RWarehouse request, string venderName)
        {
            if (request == null) return null;
            return new WarehouseViewModel
            {
                AddressDetail = request.AddressDetail,
                Code = request.Code,
                Name = request.Name,
                DistrictId = request.DistrictId,
                Email = request.Email,
                Id = request.Id,
                PhoneNumber = request.PhoneNumber,
                ProvinceId = request.ProvinceId,
                RoadId = request.RoadId,
                Status = request.Status,
                VillageId = request.VillageId,
                VendorId = request.VendorId,
                Description = request.Description,
                Type = request.Type,
                VendorName = venderName

            };
        }
        public static ProductGroupWarehouseViewModel ToModel(this RWarehouse request, string venderName, bool isAdd)
        {
            if (request == null) return null;
            return new ProductGroupWarehouseViewModel
            {
                AddressDetail = request.AddressDetail,
                Code = request.Code,
                Name = request.Name,
                DistrictId = request.DistrictId,
                Email = request.Email,
                Id = request.Id,
                PhoneNumber = request.PhoneNumber,
                ProvinceId = request.ProvinceId,
                RoadId = request.RoadId,
                Status = request.Status,
                VillageId = request.VillageId,
                VendorId = request.VendorId,
                Description = request.Description,
                Type = request.Type,
                VendorName = venderName,
                IsAdd = isAdd,

            };
        }
        public static ProductGroupProductViewModel ToModel(this RProduct product, bool isAdd)
        {
            if (product == null)
            {
                return null;
            }
            return new ProductGroupProductViewModel()
            {
                Status = product.Status,
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Code = product.Code,
                IsAdd = isAdd
            };

        }
    }
}
