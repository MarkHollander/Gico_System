using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Interfaces.Warehouse;
using Gico.SystemModels.Request;
using Gico.SystemModels.Request.Warehouse;
using Gico.SystemModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class WarehouseController : BaseController
    {


        private readonly ILogger _logger;
        private readonly IWarehouseAppService _warehouseAppService;

        public WarehouseController(ILogger<VendorController> logger, IWarehouseAppService warehouseAppService)
        {
            _logger = logger;
            _warehouseAppService = warehouseAppService;
        }

        #region Get

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] WarehouseSearchRequest request)
        {
            try
            {
                var response = await _warehouseAppService.Search(request);

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
        public async Task<IActionResult> Get([FromBody] WarehouseGetRequest request)
        {
            try
            {
                var response = await _warehouseAppService.Get(request);
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
