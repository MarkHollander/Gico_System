using System;
using System.Linq;
using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.AppService;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Response;
using Microsoft.Extensions.Logging;
using Gico.Models.Response;
using Gico.SystemAppService.Mapping;
using Gico.SystemDomains;
using Gico.SystemModels;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;
using Gico.SystemService.Interfaces;
using Polly.Retry;
using Gico.SystemAppService.Interfaces.Warehouse;
using Gico.SystemService.Interfaces.Warehouse;
using Gico.SystemModels.Response.Warehouse;
using Gico.SystemModels.Request.Warehouse;
using Gico.SystemAppService.Mapping.Warehouse;
using Gico.ReadAddressModels;
using System.Collections.Generic;
using Gico.SystemModels.Models.Warehouse;

namespace Gico.SystemAppService.Implements.Warehouse
{
    public class WarehouseAppService : IWarehouseAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<WarehouseAppService> _logger;
        private readonly IWarehouseService _warehouseService;
        private readonly ICommonService _commonService;
        private readonly ILocationService _locationService;
        private readonly IVendorService _vendorService;

        public WarehouseAppService(ILogger<WarehouseAppService> logger, IWarehouseService warehouseService, ICommonService commonService, ICurrentContext context, ILocationService locationService, IVendorService vendorService)
        {
            _logger = logger;
            _warehouseService = warehouseService;
            _commonService = commonService;
            _context = context;
            _locationService = locationService;
            _vendorService = vendorService;
        }

        public async Task<WarehouseGetResponse> Get(WarehouseGetRequest request)
        {
            WarehouseGetResponse response = new WarehouseGetResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    var warehouse = await _warehouseService.GetById(request.Id);
                    if (warehouse == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                        return response;
                    }
                    var province = await _locationService.GetProvinceById(warehouse.ProvinceId.ToString());
                    var district = await _locationService.GetDistrictById(warehouse.DistrictId.ToString());
                    var village = await _locationService.GetWardById(warehouse.VillageId.ToString());
                    RStreet road = await _locationService.GetStreetById(warehouse.RoadId.ToString());
                    response.Warehouse = warehouse.ToModel(province.ProvinceName, district.DistrictName, village.WardName, road.StreetName);
                    response.SetSucess();
                }
                else
                {
                    response.Warehouse = new WarehouseViewModel();

                }

            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<WarehouseSearchResponse> Search(WarehouseSearchRequest request)
        {
            WarehouseSearchResponse response = new WarehouseSearchResponse();
            try
            {
                List<WarehouseViewModel> warehouseViewModels = new List<WarehouseViewModel>();
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _warehouseService.Search(request.Code, request.Email, request.Phone, request.Name, request.Status, request.Type, paging);
                var venderIds = data.Select(p => p.VendorId).ToArray();
                Dictionary<string, string> vendorNameByIds = new Dictionary<string, string>();
                if (venderIds.Length > 0)
                {
                    var vendors = await _vendorService.GetFromDb(venderIds);
                    vendorNameByIds = vendors.ToDictionary(p => p.Id, p => p.Name);
                }
                response.TotalRow = paging.TotalRow;
                foreach (var item in data)
                {
                    //item.VendorName = dataVendor.Where(x => x.Id == item.VendorId.ToString()).FirstOrDefault().Name;
                    var province = await _locationService.GetProvinceById(item.ProvinceId.ToString());
                    var district = await _locationService.GetDistrictById(item.DistrictId.ToString());
                    var village = await _locationService.GetWardById(item.VillageId.ToString());
                    RStreet road = await _locationService.GetStreetById(item.RoadId.ToString());
                    string venderName = string.Empty;
                    if (!string.IsNullOrEmpty(item.VendorId))
                    {
                        venderName = vendorNameByIds.ContainsKey(item.VendorId)
                            ? vendorNameByIds[item.VendorId]
                            : string.Empty;
                    }
                    var warehouseModel = item.ToModel(province.ProvinceName, district.DistrictName, village.WardName, road.StreetName, venderName);

                    warehouseViewModels.Add(warehouseModel);
                }

                response.Warehouses = warehouseViewModels.ToArray();

                response.PageIndex = request.PageIndex;
                response.PageSize = request.PageSize;
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;

        }

    }
}
