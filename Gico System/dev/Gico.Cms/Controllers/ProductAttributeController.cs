using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.Cms.Validations;
using Gico.Config;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    [EnableCors("CorsPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProductAttributeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IProductAttributeAppService _productAttributeAppService;
        private readonly IProductAttributeValueAppService _productAttributeValueAppService;
        public ProductAttributeController(ILogger<ProductAttributeController> logger, IProductAttributeAppService productAttributeAppService, IProductAttributeValueAppService productAttributeValueAppService)
        {
            _logger = logger;
            _productAttributeAppService = productAttributeAppService;
            _productAttributeValueAppService = productAttributeValueAppService;
        }

        #region ProductAttribute

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] ProductAttributeSearchRequest request)
        {
            try
            {
                var response = await _productAttributeAppService.Search(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] ProductAttributeSearchRequest request)
        {
            try
            {
                var response = await _productAttributeAppService.Get(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] ProductAttributeCrudRequest request)
        {
            try
            {
                var response = new ProductAttributeCrudResponse();
                var validate = ProductAttributeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productAttributeAppService.AddOrUpdate(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
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
        public async Task<IActionResult> Delete([FromBody] ProductAttributeCrudRequest request)
        {
            try
            {
                var response = new ProductAttributeCrudResponse();
                var validate = ProductAttributeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    // Đổi trạng thái về Lock để ẩn thuộc tính
                    request.Status = EnumDefine.StatusEnum.Lock;

                    response = await _productAttributeAppService.AddOrUpdate(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                }
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        #endregion

        #region ProductAttributeValue

        [HttpPost]
        public async Task<IActionResult> SearchProductAttributeValue([FromBody] ProductAttributeValueSearchRequest request)
        {
            try
            {
                var response = await _productAttributeValueAppService.Search(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProductAttributeValue([FromBody] ProductAttributeValueSearchRequest request)
        {
            try
            {
                var response = await _productAttributeValueAppService.Get(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProductAttributeValue([FromBody] ProductAttributeValueCrudRequest request)
        {
            try
            {
                var response = new ProductAttributeValueCrudResponse();
                var validate = ProductAttributeValueRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productAttributeValueAppService.AddOrUpdate(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
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
        public async Task<IActionResult> DeleteProductAttributeValue([FromBody] ProductAttributeValueCrudRequest request)
        {
            try
            {
                var response = new ProductAttributeValueCrudResponse();
                var validate = ProductAttributeValueRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    // Đổi trạng thái về Lock để ẩn thuộc tính
                    request.Status = EnumDefine.StatusEnum.Lock;

                    response = await _productAttributeValueAppService.AddOrUpdate(request);
                }
                else
                {
                    response.SetFail(validate.Errors.Select(p => p.ToString()));
                }
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