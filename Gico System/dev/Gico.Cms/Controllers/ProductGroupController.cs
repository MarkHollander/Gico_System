using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Gico.Cms.Validations;
using Gico.Models.Response;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Interfaces.ProductGroup;
using Gico.SystemModels.Request;
using Gico.SystemModels.Request.ProductGroup;
using Gico.SystemModels.Response.ProductGroup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.Cms.Controllers
{
    public class ProductGroupController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IProductGroupAppService _productGroupAppService;

        public ProductGroupController(ILogger<ProductGroupController> logger, IProductGroupAppService productGroupAppService)
        {
            _logger = logger;
            _productGroupAppService = productGroupAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Gets([FromBody]ProductGroupGetsRequest request)
        {
            try
            {
                var response = await _productGroupAppService.Gets(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]ProductGroupGetRequest request)
        {
            try
            {
                ProductGroupGetResponse response = new ProductGroupGetResponse();
                ValidationResult validate = ProductGroupGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.Get(request);
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
        public async Task<IActionResult> Add([FromBody] ProductGroupAddOrChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.AddOrChange(request);
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
        public async Task<IActionResult> Change([FromBody]ProductGroupAddOrChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.AddOrChange(request);
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
        public async Task<IActionResult> GetCategories([FromBody]ProductGroupCategoryGetRequest request)
        {
            try
            {
                ProductGroupCategoryGetResponse response = new ProductGroupCategoryGetResponse();
                ValidationResult validate = ProductGroupCategoryGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetCategories(request);
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
        public async Task<IActionResult> ChangeCategories([FromBody]ProductGroupCategoryChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupCategoryChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.ChangeCategories(request);
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
        public async Task<IActionResult> GetVendors([FromBody] ProductGroupVendorGetRequest request)
        {
            try
            {
                var response = await _productGroupAppService.SearchVenders(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetVendorsConfig([FromBody]ProductGroupVendorGetRequest request)
        {
            try
            {
                ProductGroupVendorGetResponse response = new ProductGroupVendorGetResponse();
                ValidationResult validate = ProductGroupConditionGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetVenders(request);
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
        public async Task<IActionResult> AddVendor([FromBody]ProductGroupVendorAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupVendorAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.AddVendor(request);
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
        public async Task<IActionResult> RemoveVendor([FromBody]ProductGroupVendorRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupVendorAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.RemoveVendor(request);
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
        public async Task<IActionResult> GetManufacturers([FromBody] ProductGroupManufacturerGetRequest request)
        {
            try
            {
                var response = await _productGroupAppService.SearchManufacturers(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetManufacturersConfig([FromBody]ProductGroupManufacturerGetRequest request)
        {
            try
            {
                ProductGroupManufacturerGetResponse response = new ProductGroupManufacturerGetResponse();
                ValidationResult validate = ProductGroupConditionGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetManufacturers(request);
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
        public async Task<IActionResult> AddManufacturer([FromBody]ProductGroupManufacturerAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupManufacturerAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.AddManufacturer(request);
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
        public async Task<IActionResult> RemoveManufacturer([FromBody]ProductGroupManufacturerRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupManufacturerAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.RemoveManufacturer(request);
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

        [HttpGet]
        public async Task<IActionResult> GetAttributes([FromQuery]ProductGroupAttributeGetRequest request)
        {
            try
            {
                var response = await _productGroupAppService.GetAttributes(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributeValues([FromQuery]ProductGroupAttributeValueGetRequest request)
        {
            try
            {
                var response = await _productGroupAppService.GetAttributeValues(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAttributesConfig([FromBody]ProductGroupAttributesConfigGetRequest request)
        {
            try
            {
                ProductGroupAttributesConfigGetResponse response = new ProductGroupAttributesConfigGetResponse();
                ValidationResult validate = ProductGroupAttributesConfigGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetAttributesConfig(request);
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
        public async Task<IActionResult> GetAttributeConfig([FromBody]ProductGroupAttributeConfigGetRequest request)
        {
            try
            {
                ProductGroupAttributeConfigGetResponse response = new ProductGroupAttributeConfigGetResponse();
                ValidationResult validate = ProductGroupAttributeConfigGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetAttributeConfig(request);
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
        public async Task<IActionResult> AddAttributes([FromBody]ProductGroupAddOrChangeAttributeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupAddOrChangeAttributeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.AddAttributes(request);
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
        public async Task<IActionResult> ChangeAttributes([FromBody]ProductGroupAddOrChangeAttributeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupAddOrChangeAttributeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.ChangeAttributes(request);
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
        public async Task<IActionResult> RemoveAttributes([FromBody]ProductGroupRemoveAttributeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupRemoveAttributeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.RemoveAttributes(request);
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
        public async Task<IActionResult> GetPriceConfig([FromBody]ProductGroupPriceConfigGetRequest request)
        {
            try
            {
                ProductGroupPriceConfigGetResponse response = new ProductGroupPriceConfigGetResponse();
                ValidationResult validate = ProductGroupPriceConfigGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetPrices(request);
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
        public async Task<IActionResult> ChangePriceConfig([FromBody]ProductGroupPriceConfigChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupPriceConfigChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.ChangePrices(request);
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
        public async Task<IActionResult> GetQuantityConfig([FromBody]ProductGroupQuantityConfigGetRequest request)
        {
            try
            {
                ProductGroupQuantityConfigGetResponse response = new ProductGroupQuantityConfigGetResponse();
                ValidationResult validate = ProductGroupQuantityConfigGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetQuantities(request);
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
        public async Task<IActionResult> ChangeQuantityConfig([FromBody]ProductGroupQuantityConfigChangeRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupQuantityConfigChangeRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.ChangeQuantities(request);
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
        public async Task<IActionResult> GetWarehouses([FromBody] ProductGroupWarehouseGetRequest request)
        {
            try
            {
                var response = await _productGroupAppService.SearchWarehouses(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetWarehousesConfig([FromBody]ProductGroupWarehouseGetRequest request)
        {
            try
            {
                ProductGroupWarehouseGetResponse response = new ProductGroupWarehouseGetResponse();
                ValidationResult validate = ProductGroupConditionGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetWarehouses(request);
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
        public async Task<IActionResult> AddWarehouse([FromBody]ProductGroupWarehouseAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupWarehouseAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.AddWarehouse(request);
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
        public async Task<IActionResult> RemoveWarehouse([FromBody]ProductGroupWarehouseRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupWarehouseAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.RemoveWarehouse(request);
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
        public async Task<IActionResult> GetProducts([FromBody] ProductGroupProductGetRequest request)
        {
            try
            {
                var response = await _productGroupAppService.SearchProducts(request);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, Common.Common.GetMethodName(), request);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProductsConfig([FromBody]ProductGroupProductGetRequest request)
        {
            try
            {
                ProductGroupProductGetResponse response = new ProductGroupProductGetResponse();
                ValidationResult validate = ProductGroupConditionGetRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.GetProducts(request);
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
        public async Task<IActionResult> AddProduct([FromBody]ProductGroupProductAddRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupProductAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.AddProduct(request);
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
        public async Task<IActionResult> RemoveProduct([FromBody]ProductGroupProductRemoveRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse();
                ValidationResult validate = ProductGroupProductAddRequestValidator.ValidateModel(request);
                if (validate.IsValid)
                {
                    response = await _productGroupAppService.RemoveProduct(request);
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

    }
}