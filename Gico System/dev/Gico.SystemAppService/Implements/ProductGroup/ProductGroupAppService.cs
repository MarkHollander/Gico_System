using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.ReadSystemModels.Product;
using Gico.ReadSystemModels.ProductGroup;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Interfaces.ProductGroup;
using Gico.SystemAppService.Mapping;
using Gico.SystemAppService.Mapping.PageBuilder;
using Gico.SystemAppService.Mapping.Warehouse;
using Gico.SystemModels.Models;
using Gico.SystemModels.Models.ProductGroup;
using Gico.SystemModels.Models.Warehouse;
using Gico.SystemModels.Request.ProductGroup;
using Gico.SystemModels.Response;
using Gico.SystemModels.Response.ProductGroup;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.Product;
using Gico.SystemService.Interfaces.ProductGroup;
using Gico.SystemService.Interfaces.Warehouse;
using Microsoft.Extensions.Logging;

namespace Gico.SystemAppService.Implements.ProductGroup
{
    public class ProductGroupAppService : IProductGroupAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<ProductGroupAppService> _logger;
        private readonly ICommonService _commonService;
        private readonly IProductGroupService _productGroupService;
        private readonly ICategoryService _categoryService;
        private readonly IVendorService _vendorService;
        private readonly IProductAttributeService _attributeService;
        private readonly IProductAttributeValueService _attributeValueService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;

        public ProductGroupAppService(ICurrentContext context, ILogger<ProductGroupAppService> logger, ICommonService commonService, IProductGroupService productGroupService, ICategoryService categoryService, IVendorService vendorService, IProductAttributeService attributeService, IProductAttributeValueService productAttributeValueService, IManufacturerService manufacturerService, IWarehouseService warehouseService, IProductService productService)
        {
            _context = context;
            _logger = logger;
            _commonService = commonService;
            _productGroupService = productGroupService;
            _categoryService = categoryService;
            _vendorService = vendorService;
            _attributeService = attributeService;
            _attributeValueService = productAttributeValueService;
            _manufacturerService = manufacturerService;
            _warehouseService = warehouseService;
            _productService = productService;
        }

        public async Task<ProductGroupGetsResponse> Gets(ProductGroupGetsRequest request)
        {
            ProductGroupGetsResponse response = new ProductGroupGetsResponse();
            try
            {
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var productgroups = await _productGroupService.Search(request.Name, request.Status, sqlPaging);
                response.ProductGroups = productgroups.Select(p => p.ToModel()).ToArray();
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.TotalRow = sqlPaging.TotalRow;
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ProductGroupGetResponse> Get(ProductGroupGetRequest request)
        {
            ProductGroupGetResponse response = new ProductGroupGetResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    response.ProductGroup = new ProductGroupViewModel()
                    {
                        Status = EnumDefine.CommonStatusEnum.New,
                        Id = string.Empty,

                    };
                }
                else
                {
                    var productgroup = await _productGroupService.Get(request.Id);
                    response.ProductGroup = productgroup?.ToModel();
                }

                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> AddOrChange(ProductGroupAddOrChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                CommandResult result;
                if (string.IsNullOrEmpty(request.Id))
                {
                    var command = request.ToCommandAdd(user.Id);
                    result = await _productGroupService.SendCommand(command);
                }
                else
                {
                    var command = request.ToCommandChange(user.Id);
                    result = await _productGroupService.SendCommand(command);
                }
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

        public async Task<ProductGroupCategoryGetResponse> GetCategories(ProductGroupCategoryGetRequest request)
        {
            ProductGroupCategoryGetResponse response = new ProductGroupCategoryGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                string[] categoriesSettingId = new string[0];
                if (productgroup.ConditionsObject.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Category))
                {
                    categoriesSettingId = ((RProductGroupCategoryCondition)productgroup.ConditionsObject[EnumDefine.ProductGroupConfigTypeEnum.Category].Config)?.Ids;
                }

                var categories = await _categoryService.Get(request.LanguageCurrentId);
                response.Categories = categories.Select(p => p.ToJstreeStateModel(true, categoriesSettingId.Contains(p.Id), false)).ToArray();
                response.CategoryIds = categoriesSettingId;
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> ChangeCategories(ProductGroupCategoryChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandCategoryChange(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<ProductGroupVendorGetResponse> SearchVenders(ProductGroupVendorGetRequest request)
        {
            ProductGroupVendorGetResponse response = new ProductGroupVendorGetResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _vendorService.Search(request.Keyword, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.Vendors = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<ProductGroupVendorGetResponse> GetVenders(ProductGroupVendorGetRequest request)
        {
            ProductGroupVendorGetResponse response = new ProductGroupVendorGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                if (!productgroup.ConditionsObject.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Vendor))
                {
                    response.Vendors = new VendorViewModel[0];
                    response.SetSucess();
                    return response;
                }
                var vendorIds = ((RProductGroupVendorCondition)productgroup.ConditionsObject[EnumDefine.ProductGroupConfigTypeEnum.Vendor].Config).Ids;
                if (vendorIds == null || vendorIds.Length <= 0)
                {
                    response.SetSucess();
                    return response;
                }
                var vendors = (await _vendorService.GetFromDb(vendorIds) ?? new RVendor[0]) as IEnumerable<RVendor>;
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    vendors = vendors.Where(p => p.Code.Contains(request.Keyword)
                    || p.Email.Contains(request.Keyword)
                    || p.Phone.Contains(request.Keyword)
                    || p.Name.Contains(request.Keyword)
                    );
                }
                if (request.Status.AsEnumToInt() > 0)
                {
                    vendors = vendors.Where(p => p.Status == request.Status);
                }
                var vendorsArray = vendors as RVendor[] ?? vendors.ToArray();
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                response.Vendors = vendorsArray?.Skip(sqlPaging.OffSet).Take(sqlPaging.PageSize).Select(p => p.ToModel()).ToArray();
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.TotalRow = vendorsArray.Count();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> AddVendor(ProductGroupVendorAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandVenderAdd(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<BaseResponse> RemoveVendor(ProductGroupVendorRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandVenderRemove(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<ProductGroupManufacturerGetResponse> SearchManufacturers(ProductGroupManufacturerGetRequest request)
        {
            ProductGroupManufacturerGetResponse response = new ProductGroupManufacturerGetResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _manufacturerService.Search(request.Keyword, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.Manufacturers = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<ProductGroupManufacturerGetResponse> GetManufacturers(ProductGroupManufacturerGetRequest request)
        {
            ProductGroupManufacturerGetResponse response = new ProductGroupManufacturerGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                if (!productgroup.ConditionsObject.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Manufacturer))
                {
                    response.Manufacturers = new ManufacturerViewModel[0];
                    response.SetSucess();
                    return response;
                }
                var manufacturerIds = ((RProductGroupManufacturerCondition)productgroup.ConditionsObject[EnumDefine.ProductGroupConfigTypeEnum.Manufacturer].Config).Ids;
                if (manufacturerIds == null || manufacturerIds.Length <= 0)
                {
                    response.SetSucess();
                    return response;
                }
                var manufactureres = await _manufacturerService.GetById(manufacturerIds) as IEnumerable<RManufacturer>;
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    manufactureres = manufactureres.Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(request.Keyword.ToLower()));
                }
                if (request.Status.AsEnumToInt() > 0)
                {
                    manufactureres = manufactureres.Where(p => p.Status == request.Status);
                }
                response.Manufacturers = manufactureres.Select(p => p.ToModel()).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> AddManufacturer(ProductGroupManufacturerAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandManufacturerAdd(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<BaseResponse> RemoveManufacturer(ProductGroupManufacturerRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandManufacturerRemove(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<ProductGroupAttributeGetResponse> GetAttributes(ProductGroupAttributeGetRequest request)
        {
            ProductGroupAttributeGetResponse response = new ProductGroupAttributeGetResponse();
            try
            {
                RefSqlPaging sqlPaging = new RefSqlPaging(0, 100);
                var attributes = await _attributeService.Search(string.Empty, request.Keyword, sqlPaging);
                response.Attributes = attributes.Select(p => p.ToKeyValueModel()).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ProductGroupAttributeValueGetResponse> GetAttributeValues(ProductGroupAttributeValueGetRequest request)
        {
            ProductGroupAttributeValueGetResponse response = new ProductGroupAttributeValueGetResponse();
            try
            {
                RefSqlPaging sqlPaging = new RefSqlPaging(0, 100);
                var attributeValues = await _attributeValueService.Search(request.AttributeId, string.Empty, request.Keyword, sqlPaging);
                response.AttributeValues = attributeValues.Select(p => p.ToKeyValueModel()).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ProductGroupAttributesConfigGetResponse> GetAttributesConfig(ProductGroupAttributesConfigGetRequest request)
        {
            ProductGroupAttributesConfigGetResponse response = new ProductGroupAttributesConfigGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                if (!productgroup.ConditionsObject.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Attribute))
                {
                    response.Attributes = new ProductAttributeViewModel[0];
                    response.SetSucess();
                    return response;
                }
                RProductGroupAttributeCondition attributeCondition = (RProductGroupAttributeCondition)productgroup.ConditionsObject[EnumDefine.ProductGroupConfigTypeEnum.Attribute].Config;
                if (attributeCondition?.Attributes == null || attributeCondition.Attributes?.Length <= 0)
                {
                    response.Attributes = new ProductAttributeViewModel[0];
                    response.SetSucess();
                    return response;
                }
                var attributeIds = attributeCondition.Attributes.Select(p => p.AttributeId).ToArray();
                var attributes = await _attributeService.GetFromDb(attributeIds) as IEnumerable<RProductAttribute>;
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    attributes = attributes.Where(p => p.AttributeName.ToLower().Contains(request.Keyword.ToLower()));
                }
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                attributes = attributes.OrderBy(p => p.AttributeName).Skip(sqlPaging.OffSet).Take(sqlPaging.PageSize).ToArray();
                var attributeValueIds = attributeCondition.Attributes.Where(p => attributes.Any(q => q.AttributeId == p.AttributeId)).SelectMany(p => p.AttributeValueIds).Distinct()
                    .ToArray();
                var attributeValues = await _attributeValueService.GetFromDb(attributeValueIds);
                IList<ProductAttributeViewModel> attributeViewModels = new List<ProductAttributeViewModel>();
                foreach (var rProductGroupAttributeDetailCondition in attributeCondition.Attributes)
                {
                    var attribute = attributes.FirstOrDefault(p => p.AttributeId == rProductGroupAttributeDetailCondition.AttributeId);
                    if (attribute == null)
                    {
                        continue;
                    }
                    var attributeValue = attributeValues.Where(p => rProductGroupAttributeDetailCondition.AttributeValueIds.Contains(p.AttributeValueId)).ToArray();
                    attributeViewModels.Add(attribute.ToModel(attributeValue));
                }
                response.Attributes = attributeViewModels.OrderBy(p => p.AttributeName).ToArray();
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.TotalRow = (attributeCondition?.Attributes?.Length).GetValueOrDefault();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ProductGroupAttributeConfigGetResponse> GetAttributeConfig(ProductGroupAttributeConfigGetRequest request)
        {
            ProductGroupAttributeConfigGetResponse response = new ProductGroupAttributeConfigGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                if (!productgroup.ConditionsObject.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Attribute))
                {
                    response.SetFail("Attribute not exist.");
                    return response;
                }
                RProductGroupAttributeCondition attributeCondition = (RProductGroupAttributeCondition)productgroup.ConditionsObject[EnumDefine.ProductGroupConfigTypeEnum.Attribute].Config;
                if (attributeCondition?.Attributes == null || attributeCondition.Attributes?.Length <= 0)
                {
                    response.SetFail("Attribute not exist.");
                    return response;
                }
                var attributeConfig =
                    attributeCondition.Attributes.FirstOrDefault(p => p.AttributeId == request.AttributeId);
                if (attributeConfig == null)
                {
                    response.SetFail("Attribute not exist.");
                    return response;
                }
                var attribute = await _attributeService.GetFromDb(request.AttributeId);
                if (attribute == null)
                {
                    response.SetFail("Attribute not exist.");
                    return response;
                }
                var attributeValueIds = attributeConfig.AttributeValueIds;
                var attributeValues = await _attributeValueService.GetFromDb(attributeValueIds);
                response.Attribute = attribute.ToModel(attributeValues);
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> AddAttributes(ProductGroupAddOrChangeAttributeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandAttributeAdd(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<BaseResponse> ChangeAttributes(ProductGroupAddOrChangeAttributeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandAttributeChange(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<BaseResponse> RemoveAttributes(ProductGroupRemoveAttributeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandAttributeRemove(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<ProductGroupPriceConfigGetResponse> GetPrices(ProductGroupPriceConfigGetRequest request)
        {
            ProductGroupPriceConfigGetResponse response = new ProductGroupPriceConfigGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                if (!productgroup.ConditionsObject.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Price))
                {
                    response.Prices = new ProductGroupPriceConfigModel[0];
                    response.SetSucess();
                    return response;
                }
                RProductPriceCondition attributeCondition = (RProductPriceCondition)productgroup.ConditionsObject[EnumDefine.ProductGroupConfigTypeEnum.Price].Config;
                if (attributeCondition?.Prices == null || attributeCondition.Prices?.Length <= 0)
                {
                    response.Prices = new ProductGroupPriceConfigModel[0];
                    response.SetSucess();
                    return response;
                }
                response.Prices = attributeCondition.Prices.ToModel().OrderBy(p => p.MinPrice).ThenBy(p => p.MaxPrice).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> ChangePrices(ProductGroupPriceConfigChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandPriceChange(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<ProductGroupQuantityConfigGetResponse> GetQuantities(ProductGroupQuantityConfigGetRequest request)
        {
            ProductGroupQuantityConfigGetResponse response = new ProductGroupQuantityConfigGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                if (!productgroup.ConditionsObject.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Quantity))
                {
                    response.Quantities = new ProductGroupQuantityConfigModel[0];
                    response.SetSucess();
                    return response;
                }
                RProductQuantityCondition attributeCondition = (RProductQuantityCondition)productgroup.ConditionsObject[EnumDefine.ProductGroupConfigTypeEnum.Quantity].Config;
                if (attributeCondition?.Quantities == null || attributeCondition.Quantities?.Length <= 0)
                {
                    response.Quantities = new ProductGroupQuantityConfigModel[0];
                    response.SetSucess();
                    return response;
                }
                response.Quantities = attributeCondition.Quantities.ToModel().OrderBy(p => p.MinQuantity).ThenBy(p => p.MaxQuantity).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> ChangeQuantities(ProductGroupQuantityConfigChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandQuantityChange(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<ProductGroupWarehouseGetResponse> SearchWarehouses(ProductGroupWarehouseGetRequest request)
        {
            try
            {
                ProductGroupWarehouseGetResponse response = new ProductGroupWarehouseGetResponse();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var key = EnumDefine.ProductGroupConfigTypeEnum.Warehouse;
                string[] warehouseIdsExist = new string[0];
                if (productgroup.ConditionsObject.ContainsKey(key))
                {
                    var warehouseIds = ((RProductGroupWarehouseCondition)productgroup.ConditionsObject[key].Config).Ids;
                    if (warehouseIds != null && warehouseIds.Length > 0)
                    {
                        warehouseIdsExist = warehouseIds;
                    }

                }
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var warehouses = await _warehouseService.Search(request.VenderId, request.Keyword, request.Status, request.Type, sqlPaging);
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.TotalRow = sqlPaging.TotalRow;
                var venderIds = warehouses.Select(p => p.VendorId).Distinct().ToArray();
                Dictionary<string, string> vendorNameByIds = new Dictionary<string, string>();
                if (venderIds.Length > 0)
                {
                    var vendors = await _vendorService.GetFromDb(venderIds);
                    vendorNameByIds = vendors.ToDictionary(p => p.Id, p => p.Name);
                }
                response.Warehouses = warehouses.Select(p => p.ToModel(
                    (vendorNameByIds.ContainsKey(p.VendorId) && !string.IsNullOrEmpty(p.VendorId)) ? vendorNameByIds[p.VendorId] : string.Empty,
                    !warehouseIdsExist.Contains(p.Id)
                    )).ToArray();
                response.SetSucess();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ProductGroupWarehouseGetResponse> GetWarehouses(ProductGroupWarehouseGetRequest request)
        {
            ProductGroupWarehouseGetResponse response = new ProductGroupWarehouseGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var key = EnumDefine.ProductGroupConfigTypeEnum.Warehouse;
                if (!productgroup.ConditionsObject.ContainsKey(key))
                {
                    response.Warehouses = new ProductGroupWarehouseViewModel[0];
                    response.SetSucess();
                    return response;
                }
                var warehouseIds = ((RProductGroupWarehouseCondition)productgroup.ConditionsObject[key].Config).Ids;
                if (warehouseIds == null || warehouseIds.Length <= 0)
                {
                    response.SetSucess();
                    return response;
                }
                var warehouses = (await _warehouseService.GetById(warehouseIds)) as IEnumerable<Gico.ReadSystemModels.Warehouse.RWarehouse>;
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    warehouses = warehouses.Where(p => p.Name.Contains(request.Keyword)
                    || p.PhoneNumber.Contains(request.Keyword)
                    || p.Code.Contains(request.Keyword)
                    || p.Email.Contains(request.Keyword)
                    );
                }
                if (!string.IsNullOrEmpty(request.VenderId))
                {
                    warehouses = warehouses.Where(p => p.VendorId == request.VenderId);
                }
                if (request.Status.AsEnumToInt() > 0)
                {
                    warehouses = warehouses.Where(p => p.Status == request.Status);
                }
                if (request.Type.AsEnumToInt() > 0)
                {
                    warehouses = warehouses.Where(p => p.Type == request.Type);
                }
                var warehousesArray = warehouses as Gico.ReadSystemModels.Warehouse.RWarehouse[] ?? warehouses.ToArray();
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);

                var venderIds = warehousesArray.Select(p => p.VendorId).Distinct().ToArray();
                Dictionary<string, string> vendorNameByIds = new Dictionary<string, string>();
                if (venderIds.Length > 0)
                {
                    var vendors = await _vendorService.GetFromDb(venderIds);
                    vendorNameByIds = vendors.ToDictionary(p => p.Id, p => p.Name);
                }
                response.Warehouses = warehousesArray?.Skip(sqlPaging.OffSet).Take(sqlPaging.PageSize).Select(p => p.ToModel(
                    (vendorNameByIds.ContainsKey(p.VendorId) && !string.IsNullOrEmpty(p.VendorId)) ? vendorNameByIds[p.VendorId] : string.Empty,
                    false
                    )).ToArray();
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.TotalRow = warehousesArray.Length;
                response.SetSucess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }

        public async Task<BaseResponse> AddWarehouse(ProductGroupWarehouseAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandWarehouseAdd(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<BaseResponse> RemoveWarehouse(ProductGroupWarehouseRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandWarehouseRemove(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<ProductGroupProductGetResponse> SearchProducts(ProductGroupProductGetRequest request)
        {
            ProductGroupProductGetResponse response = new ProductGroupProductGetResponse();
            try
            {

                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var key = EnumDefine.ProductGroupConfigTypeEnum.Product;
                string[] productIdsExist = new string[0];
                if (productgroup.ConditionsObject.ContainsKey(key))
                {
                    productIdsExist = ((RProductIdsCondition)productgroup.ConditionsObject[key].Config).Ids ?? new string[0];

                }
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var products = await _productService.SearchByCodeAndName(request.Keyword, request.Status, sqlPaging);
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                response.TotalRow = sqlPaging.TotalRow;
                response.Products = products.Select(p => p.ToModel(!productIdsExist.Contains(p.Id))).ToArray();
                response.SetSucess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }

        public async Task<ProductGroupProductGetResponse> GetProducts(ProductGroupProductGetRequest request)
        {
            ProductGroupProductGetResponse response = new ProductGroupProductGetResponse();
            try
            {
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var key = EnumDefine.ProductGroupConfigTypeEnum.Product;
                if (!productgroup.ConditionsObject.ContainsKey(key))
                {
                    response.Products = new ProductGroupProductViewModel[0];
                    response.SetSucess();
                    return response;
                }
                var productIds = ((RProductIdsCondition)productgroup.ConditionsObject[key].Config).Ids;
                if (productIds == null || productIds.Length <= 0)
                {
                    response.SetSucess();
                    return response;
                }
                var products = await _productService.GetById(productIds) as IEnumerable<RProduct>;
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    products = products.Where(p => p.Name.Contains(request.Keyword) || p.Code.Contains(request.Keyword));
                }
                if (request.Status.AsEnumToInt() > 0)
                {
                    products = products.Where(p => p.Status == request.Status);
                }
                if (request.Type.AsEnumToInt() > 0)
                {
                    products = products.Where(p => p.Type == request.Type);
                }
                products = products.OrderBy(p => p.Name);
                var productsArray = products.ToArray();
                RefSqlPaging sqlPaging = new RefSqlPaging(request.PageIndex, request.PageSize);

                response.Products = productsArray?.Skip(sqlPaging.OffSet).Take(sqlPaging.PageSize).Select(p => p.ToModel(false)).ToArray();
                response.PageIndex = sqlPaging.PageIndex;
                response.PageSize = sqlPaging.PageSize;
                if (productsArray != null) response.TotalRow = productsArray.Length;
                response.SetSucess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return response;
        }

        public async Task<BaseResponse> AddProduct(ProductGroupProductAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandProductAdd(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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

        public async Task<BaseResponse> RemoveProduct(ProductGroupProductRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var productgroup = await _productGroupService.Get(request.ProductGroupId);
                if (productgroup == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.ProductGroupNotFound);
                    return response;
                }
                var command = request.ToCommandProductRemove(user.Id);
                CommandResult result = await _productGroupService.SendCommand(command);
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
    }
}
