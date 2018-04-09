using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Gico.SystemAppService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class LocationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILocationAppService _locationAppService;

        public LocationController(ILogger<LocationController> logger, ILocationAppService locationAppService)
        {
            _logger = logger;
            _locationAppService = locationAppService;
        }

        #region GET

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetAllProvince(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Districs([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetDistrcisByProvinceId(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Ward([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetWardByDistricId(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Street([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetStreetByWardId(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetProvinceById([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetProvinceById(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetDistrictById([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetDistrictById(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetWardById([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetWardById(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetStreetById([FromBody] LocationSearchRequest request)
        {
            try
            {
                var response = await _locationAppService.GetStreetById(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }

        #endregion

        #region CRUD

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ProvinceUpdate([FromBody] LocationUpdateRequest request)
        {
            try
            {
                LocationUpdateResponse response = new LocationUpdateResponse();
                var results = ProvinceUpdateRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _locationAppService.ProvinceUpdate(request);
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> DistrictUpdate([FromBody] LocationUpdateRequest request)
        {
            try
            {
                LocationUpdateResponse response = new LocationUpdateResponse();
                var results = DistrictRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _locationAppService.DistrictUpdate(request);
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> WardUpdate([FromBody] LocationUpdateRequest request)
        {
            try
            {
                LocationUpdateResponse response = new LocationUpdateResponse();
                var results = WardRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _locationAppService.WardUpdate(request);
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]LocationRemoveRequest request)
        {
            try
            {
                var response = new BaseResponse();
                response = await _locationAppService.LocationRemove(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        #endregion

    }
}