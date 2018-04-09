using System.Threading.Tasks;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Request;
using System;
using System.Linq;
using Gico.SystemDomains;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Mapping;
using Gico.SystemModels.Models;
using Gico.AppService;
using Gico.Config;

namespace Gico.SystemAppService.Implements
{
    public class AttrCategoryAppService : IAttrCategoryAppService
    {

        private readonly ICurrentContext _context;
        private readonly IAttrCategoryService _attrCategoryService;
        private readonly ILogger<AttrCategoryAppService> _logger;
        private readonly ICommonService _commonService;
        private readonly IProductAttributeService _productAttributeService;
        public AttrCategoryAppService(ILogger<AttrCategoryAppService> logger, IAttrCategoryService attrCategoryService, ICurrentContext context, ICommonService commonService, IProductAttributeService productAttributeService)
        {
            _logger = logger;
            _attrCategoryService = attrCategoryService;
            _context = context;
            _commonService = commonService;
            _productAttributeService = productAttributeService;
        }
        public async Task<AttrCategoryAddOrChangeResponse> AttrCategoryAdd(AttrCategoryAddRequest request)
        {
            AttrCategoryAddOrChangeResponse response = new AttrCategoryAddOrChangeResponse();
            try
            {

                var command = request.AttrCategory.ToAddCommand();

                var result = await _attrCategoryService.SendCommand(command);
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

        public async Task<AttrCategoryAddOrChangeResponse> AttrCategoryChange(AttrCategoryChangeRequest request)
        {
            AttrCategoryAddOrChangeResponse response = new AttrCategoryAddOrChangeResponse();
            try
            {
                var command = request.AttrCategory.ToChangeCommand();
                var result = await _attrCategoryService.SendCommand(command);
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

        public async Task<AttrCategoryGetResponse> AttrCategoryGet(AttrCategoryGetRequest request)
        {

            AttrCategoryGetResponse response = new AttrCategoryGetResponse();
            try
            {
                var attrCategory = await _attrCategoryService.Get(request.AttributeId, request.CategoryId);
                if (attrCategory == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                    return response;
                }
                response.AttrCategory = attrCategory.ToModel();
                RProductAttribute[] productAttributes = await _attrCategoryService.GetsProductAttr(request.CategoryId);
                response.ProductAttributes = productAttributes?.Select(p => p.ToModel()).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<AttrCategoryRemoveResponse> AttrCategoryRemove(AttrCategoryRemoveRequest request)
        {
            AttrCategoryRemoveResponse response = new AttrCategoryRemoveResponse();
            try
            {
                var attrCategory = new AttrCategoryModel
                {
                    AttributeId = request.AttributeId,
                    CategoryId = request.CategoryId
                };
                var command = attrCategory.ToRemoveCommand();

                var result = await _attrCategoryService.SendCommand(command);
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

        public async Task<AttrCategoryMapping_GetsProductAttrResponse> GetsProductAttr(AttrCategoryMapping_GetsProductAttrRequest request)
        {
            AttrCategoryMapping_GetsProductAttrResponse response = new AttrCategoryMapping_GetsProductAttrResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.CategoryId))
                {
                    RProductAttribute[] productAttributes = await _attrCategoryService.GetsProductAttr(request.CategoryId);
                    if (productAttributes == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.CategoryNotFound);
                        return response;
                    }
                    response.ProductAttributes = productAttributes?.Select(p => p.ToModel()).ToArray();
                   
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
    }
}
