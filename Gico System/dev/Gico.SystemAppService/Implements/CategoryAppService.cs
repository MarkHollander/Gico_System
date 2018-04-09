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
    public class CategoryAppService:ICategoryAppService
    {
        private readonly ICurrentContext _context;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryAppService> _logger;
        private readonly ILanguageService _languageService;
        private readonly ICommonService _commonService;
        public CategoryAppService(ILogger<CategoryAppService> logger, ILanguageService languageService, ICategoryService categoryService, ICurrentContext context, ICommonService commonService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _languageService = languageService;
            _context = context;
            _commonService = commonService;
        }

        public async Task<CategoryGetsResponse> CategoryGet(CategoryGetsRequest request)
        {
            CategoryGetsResponse response = new CategoryGetsResponse();
            try
            {
                RCategory[] categories = await _categoryService.Get(request.LanguageCurrentId);
                if (categories.Length > 0)
                {
                    response.Categories = categories?.Select(p => p.ToModel()).ToArray();
                }
                RLanguage[] languages = await _languageService.Get();
                if (languages.Length > 0)
                {
                    response.Languages = languages.Select(p => p.ToKeyValueModel()).ToArray();
                }
                response.LanguageDefaultId = "2";
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<CategoryGetResponse> CategoryGet(CategoryGetRequest request)
        {

            CategoryGetResponse response = new CategoryGetResponse();
            try
            {
                RCategory[] categories = await _categoryService.Get(request.LanguageCurrentId);
                if (categories.Length > 0)
                {
                    if (!string.IsNullOrEmpty(request.Id))
                    {
                        RCategory currentCategory = categories.FirstOrDefault(p => p.Id == request.Id);
                        if (currentCategory != null)
                        {
                            categories = RCategory.RemoveCurrentTree(categories, currentCategory);
                        }
                    }
                    response.Parents = categories?.Select(p => p.ToModel()).ToArray();
                }
                RLanguage[] languages = await _languageService.Get();
                if (languages.Length > 0)
                {
                    response.Languages = languages.Select(p => p.ToKeyValueModel()).ToArray();
                }
                if (!string.IsNullOrEmpty(request.Id))
                {
                    RCategory category = await _categoryService.Get(request.LanguageId, request.Id);
                    if (category == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.CategoryNotFound);
                        return response;
                    }
                    response.Category = category.ToModel();
                }
                else
                {
                    response.Category = new CategoryModel();
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

        public async Task<CategoryAddOrChangeResponse> CategoryAddOrChange(CategoryAddOrChangeRequest request)
        {
            CategoryAddOrChangeResponse response = new CategoryAddOrChangeResponse();
            try
            {
                //var userLogin = await _context.GetCurrentCustomer();

                if (string.IsNullOrEmpty(request.Category.Id))
                {
                    long systemIdentity = await _commonService.GetNextId(typeof(Customer));
                    string code = Common.Common.GenerateCodeFromId(systemIdentity, 3);
                    //  var command = request.ToCommand(_context.Ip, userLogin.Id, code);

                    var command = request.Category.ToAddCommand("123", code);
                  
                    var result = await _categoryService.SendCommand(command);
                    if (result.IsSucess)
                    {
                        response.SetSucess();
                    }
                    else
                    {
                        response.SetFail(result.Message);
                    }
                }
                else
                {
                    var command = request.Category.ToChangeCommand(request.Category.Version);
                    var result = await _categoryService.SendCommand(command);
                    if (result.IsSucess)
                    {
                        response.SetSucess();
                    }
                    else
                    {
                        response.SetFail(result.Message);
                    }
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<CategoryAttrResponse> GetListAttr(CategoryAttrRequest request)
        {
            CategoryAttrResponse response = new CategoryAttrResponse();

            
            try
            {
                RefSqlPaging sqlpaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _categoryService.GetListAttr(request.Id, sqlpaging);
                 response.TotalRow = sqlpaging.TotalRow;
                response.CategoryAttrs = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<CategoryManufacturerGetListResponse> GetListManufacturer(CategoryManufacturerGetListRequest request)
        {
            CategoryManufacturerGetListResponse response = new CategoryManufacturerGetListResponse();


            try
            {
                RefSqlPaging sqlpaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _categoryService.GetListManufacturer(request.Id, sqlpaging);
                response.TotalRow = sqlpaging.TotalRow;
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

    }
}
