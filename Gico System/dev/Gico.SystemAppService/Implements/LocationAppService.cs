using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.ReadAddressModels;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Gico.SystemModels.Mapping;
using Microsoft.Extensions.Logging;
using Gico.AppService;
using Gico.Models.Response;

namespace Gico.SystemAppService.Implements
{
    public class LocationAppService : ILocationAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationAppService> _logger;

        public LocationAppService(ILocationService locationService, ILogger<LocationAppService> logger, ICurrentContext context)
        {
            _locationService = locationService;
            _logger = logger;
            _context = context;
        }

        #region GET

        public async Task<LocationSearchResponse> GetAllProvince(LocationSearchRequest request)
        {
            LocationSearchResponse response = new LocationSearchResponse();
            try
            {
                var data = await _locationService.ProvinceGetAllFromDb(request.Name);
                response.LtsProvince = data.Select(x => x.ToModel()).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationSearchResponse> GetDistrcisByProvinceId(LocationSearchRequest request)
        {
            LocationSearchResponse response = new LocationSearchResponse();
            try
            {
                var data = await _locationService.DistrictGetByProvinceIdFromDb(request.ProvinceId, request.Name);
                response.LtsDictrics = data.Select(x => x.ToModelDistric()).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationSearchResponse> GetWardByDistricId(LocationSearchRequest request)
        {
            LocationSearchResponse response = new LocationSearchResponse();
            try
            {
                var data = await _locationService.WardGetByDistrictIdFromDb(request.DistricId, request.Name);
                response.LtsWard = data.Select(x => x.ToModelWard()).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationSearchResponse> GetStreetByWardId(LocationSearchRequest request)
        {
            LocationSearchResponse response = new LocationSearchResponse();
            try
            {
                var data = await _locationService.StreetGetByWardIdFromDb(request.WardId);
                response.LtsStreet = data.Select(x => x.ToModelStreet()).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationDetailResponse> GetWardById(LocationSearchRequest request)
        {
            LocationDetailResponse response = new LocationDetailResponse();
            try
            {
                var data = await _locationService.GetWardById(request.WardId);
                response = data.ToModelWardDetail();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationDetailResponse> GetStreetById(LocationSearchRequest request)
        {
            LocationDetailResponse response = new LocationDetailResponse();
            try
            {
                var data = await _locationService.GetStreetById(request.StreetId);
                response = data.ToModelStreetDetail();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationDetailResponse> GetProvinceById(LocationSearchRequest request)
        {
            LocationDetailResponse response = new LocationDetailResponse();
            try
            {
                var data = await _locationService.GetProvinceById(request.ProvinceId);
                response = data.ToModelDetail();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationDetailResponse> GetDistrictById(LocationSearchRequest request)
        {
            LocationDetailResponse response = new LocationDetailResponse();
            try
            {
                var data = await _locationService.GetDistrictById(request.DistricId);
                response = data.ToModelDistrictDetail();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        #endregion

        #region CRUD

        public async Task<LocationUpdateResponse> ProvinceUpdate(LocationUpdateRequest request)
        {
            LocationUpdateResponse response = new LocationUpdateResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var command = request.ToUpdateCommand(user.Id);
                var result = await _locationService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationUpdateResponse> DistrictUpdate(LocationUpdateRequest request)
        {
            LocationUpdateResponse response = new LocationUpdateResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var command = request.ToUpdateDistrictCommand(user.Id);
                var result = await _locationService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<LocationUpdateResponse> WardUpdate(LocationUpdateRequest request)
        {
            LocationUpdateResponse response = new LocationUpdateResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var command = request.ToUpdateWardCommand(user.Id);
                var result = await _locationService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> LocationRemove(LocationRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var command = request.ToCommandRemove(user.Id);
                var result = await _locationService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        #endregion

    }
}