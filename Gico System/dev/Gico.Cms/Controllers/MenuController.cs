using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Interfaces.PageBuilder;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class MenuController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMenuAppService _menuAppService;
        public MenuController(ILogger<MenuController> logger, IMenuAppService menuAppService)
        {
            _logger = logger;
            _menuAppService = menuAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Gets([FromBody] MenuGetsRequest request)
        {
            try
            {
                var response = await _menuAppService.MenuGet(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] MenuGetRequest request)
        {
            try
            {
                MenuGetResponse response = new MenuGetResponse();
                var results = MenuGetRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _menuAppService.MenuGet(request);
                }
                else
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName());
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrChange([FromBody] MenuAddOrChangeRequest request)
        {
            try
            {
                MenuAddOrChangeResponse response = new MenuAddOrChangeResponse();
                var results = MenuAddOrChangeRequestValidator.ValidateModel(request);
                if (results.IsValid)
                {
                    results = MenuModelAddModelValidator.ValidateModel(request.Menu);
                    if (results.IsValid)
                    {
                        response = await _menuAppService.MenuAddOrChange(request);
                    }
                    else
                    {
                        response.SetFail(results.Errors.Select(p => p.ToString()));
                    }
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
        public async Task<IActionResult> GetBanners([FromBody] BannerGetByMenuIdRequest request)
        {
            try
            {
                BannersGetByMenuIdResponse response = new BannersGetByMenuIdResponse();
                var results = BannersGetByMenuIdRequestValidate.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _menuAppService.BannerGetByMenuId(request);
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
        public async Task<IActionResult> AddBanner([FromBody] MenuBannerMappingAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                var results = MenuBannerMappingAddRequestValidate.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _menuAppService.MenuBannerMappingAdd(request);
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
        public async Task<IActionResult> RemoveBanner([FromBody] MenuBannerMappingRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                var results = MenuBannerMappingAddRequestValidate.ValidateModel(request);
                if (results.IsValid)
                {
                    response = await _menuAppService.MenuBannerMappingRemove(request);
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
    }
}