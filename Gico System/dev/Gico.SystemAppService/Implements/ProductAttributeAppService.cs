using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Config;
using Gico.Models.Response;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Response;
using Microsoft.Extensions.Logging;
using Gico.SystemAppService.Mapping;
using Gico.SystemModels.Request;
using Gico.SystemService.Interfaces;

namespace Gico.SystemAppService.Implements
{
    public class ProductAttributeAppService : IProductAttributeAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<ProductAttributeAppService> _logger;
        private readonly ICommonService _commonService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly ILanguageService _languageService;

        public ProductAttributeAppService(ILogger<ProductAttributeAppService> logger, ICommonService commonService, IProductAttributeService productAttributeService, ILanguageService languageService, ICurrentContext context)
        {
            _logger = logger;
            _commonService = commonService;
            _productAttributeService = productAttributeService;
            _languageService = languageService;
            _context = context;
        }

        public async Task<ProductAttributeSearchResponse> Search(ProductAttributeSearchRequest request)
        {
            var response = new ProductAttributeSearchResponse();
            try
            {
                var paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _productAttributeService.Search(request.AttributeId, request.AttributeName, paging);
                response.ProductAttributes = data.Select(x => x.ToModel()).ToArray();
                response.TotalRow = paging.TotalRow;
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

        public async Task<ProductAttributeGetResponse> Get(ProductAttributeSearchRequest request)
        {
            var response = new ProductAttributeGetResponse();
            try
            {
                if (request != null)
                {
                    var result = await _productAttributeService.GetFromDb(request.AttributeId);
                    if (result == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.Fail);
                        return response;
                    }
                    response.ProductAttribute = result.ToModel();
                }
                else
                {
                    response.ProductAttribute = new ProductAttributeViewModel()
                    {
                        AttributeStatus = 0
                    };
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ProductAttributeCrudResponse> AddOrUpdate(ProductAttributeCrudRequest request)
        {
            var response = new ProductAttributeCrudResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var command = request.ToCommand(user.Id);
                var result = await _productAttributeService.SendCommand(command);
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

    public class ProductAttributeValueAppService : IProductAttributeValueAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<ProductAttributeAppService> _logger;
        private readonly ICommonService _commonService;
        private readonly IProductAttributeValueService _productAttributeValue;
        private readonly ILanguageService _languageService;

        public ProductAttributeValueAppService(ILogger<ProductAttributeAppService> logger, ICommonService commonService, IProductAttributeValueService productAttributeValueService, ILanguageService languageService, ICurrentContext context)
        {
            _logger = logger;
            _commonService = commonService;
            _productAttributeValue = productAttributeValueService;
            _languageService = languageService;
            _context = context;
        }

        public async Task<ProductAttributeValueSearchResponse> Search(ProductAttributeValueSearchRequest request)
        {
            var response = new ProductAttributeValueSearchResponse();
            try
            {
                var paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _productAttributeValue.Search(request.AttributeId, request.AttributeValueId, request.Value, paging);
                response.ProductAttributeValues = data.Select(x => x.ToModel()).ToArray();
                response.TotalRow = paging.TotalRow;
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

        public async Task<ProductAttributeValueGetResponse> Get(ProductAttributeValueSearchRequest request)
        {
            var response = new ProductAttributeValueGetResponse();
            try
            {
                if (request != null)
                {
                    var result = await _productAttributeValue.GetFromDb(request.AttributeValueId);
                    if (result == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.Fail);
                        return response;
                    }
                    response.ProductAttributeValue = result.ToModel();
                }
                else
                {
                    response.ProductAttributeValue = new ProductAttributeValueViewModel()
                    {
                        AttributeValueStatus = 0
                    };
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ProductAttributeValueCrudResponse> AddOrUpdate(ProductAttributeValueCrudRequest request)
        {
            var response = new ProductAttributeValueCrudResponse();
            try
            {
                var user = await _context.GetCurrentCustomer();
                var command = request.ToCommand(user.Id);
                var result = await _productAttributeValue.SendCommand(command);
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