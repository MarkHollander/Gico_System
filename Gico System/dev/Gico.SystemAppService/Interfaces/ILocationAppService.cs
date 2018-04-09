using Gico.ReadAddressModels;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.Models.Response;
using Gico.SystemModels.Request.Banner;

namespace Gico.SystemAppService.Interfaces
{
    public interface ILocationAppService
    {
        Task<LocationSearchResponse> GetAllProvince(LocationSearchRequest request);
        Task<LocationSearchResponse> GetDistrcisByProvinceId(LocationSearchRequest request);
        Task<LocationSearchResponse> GetWardByDistricId(LocationSearchRequest request);
        Task<LocationSearchResponse> GetStreetByWardId(LocationSearchRequest request);
        Task<LocationDetailResponse> GetProvinceById(LocationSearchRequest request);
        Task<LocationDetailResponse> GetDistrictById(LocationSearchRequest request);
        Task<LocationDetailResponse> GetWardById(LocationSearchRequest request);
        Task<LocationDetailResponse> GetStreetById(LocationSearchRequest request);
        Task<LocationUpdateResponse> ProvinceUpdate(LocationUpdateRequest request);
        Task<LocationUpdateResponse> DistrictUpdate(LocationUpdateRequest request);
        Task<LocationUpdateResponse> WardUpdate(LocationUpdateRequest request);
        Task<BaseResponse> LocationRemove(LocationRemoveRequest request);
    }
}