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
    public class VariationThemeAppService : IVariationThemeAppService
    {
        private readonly ICurrentContext _context;
        private readonly IVariationThemeService _variationThemeService;
        private readonly ILogger<VariationThemeAppService> _logger;
        private readonly ICommonService _commonService;

        public VariationThemeAppService(ILogger<VariationThemeAppService> logger, IVariationThemeService variationThemeService, ICurrentContext context, ICommonService commonService)
        {
            _logger = logger;
            _variationThemeService = variationThemeService;
            _context = context;
            _commonService = commonService;
        }

        public async Task<Category_VariationTheme_Mapping_RemoveResponse> Remove(Category_VariationTheme_Mapping_RemoveRequest request)
        {
            Category_VariationTheme_Mapping_RemoveResponse response = new Category_VariationTheme_Mapping_RemoveResponse();
            try
            {
                var categoryVariationThemeMapping = new CategoryVariationThemeMappingModel
                {
                    VariationThemeId = request.VariationThemeId,
                   CategoryId = request.CategoryId
                };
                var command = categoryVariationThemeMapping.ToRemoveCommand();

                var result = await _variationThemeService.SendCommand(command);
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

        public async Task<Category_VariationTheme_MappingAddResponse> Category_VariationTheme_MappingAdd(Category_VariationTheme_MappingAddRequest request)
        {
            Category_VariationTheme_MappingAddResponse response = new Category_VariationTheme_MappingAddResponse();

            try
            {

                var command = request.Category_VariationTheme_Mapping.ToAddCommand();

                var result = await _variationThemeService.SendCommand(command);
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

        public async Task<Category_VariationTheme_MappingGetsResponse> Category_VariationTheme_MappingGets(Category_VariationTheme_MappingGetsRequest request)
        {
            Category_VariationTheme_MappingGetsResponse response = new Category_VariationTheme_MappingGetsResponse();
            try
            {
                RCategory_VariationTheme_Mapping[] rCategory_VariationTheme_Mapping = await _variationThemeService.Get(request.CategoryId);
                if (rCategory_VariationTheme_Mapping.Length > 0)
                {
                    response.CategoryVariationThemeMapping = rCategory_VariationTheme_Mapping?.Select(p => p.ToModel()).ToArray();
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

        public async Task<VariationThemeGetResponse> VariationThemeGet(VariationThemeGetRequest request)
        {
            VariationThemeGetResponse response = new VariationThemeGetResponse();
            try
            {
                RVariationTheme[] variationThemes = await _variationThemeService.Get();
                if (variationThemes.Length > 0)
                {
                    response.VariationThemes = variationThemes?.Select(p => p.ToKeyValueModel()).ToArray();
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

        public async Task<VariationTheme_AttributeGetResponse> VariationTheme_AttributeGet(VariationTheme_AttributeGetRequest request)
        {
            VariationTheme_AttributeGetResponse response = new VariationTheme_AttributeGetResponse();
            try
            {
                RVariationTheme rVariationTheme = await _variationThemeService.GetVariationThemeById(request.Id);
                string variationThemeName = rVariationTheme.ToModel().VariationThemeName;
                RVariationTheme_Attribute[] variationTheme_Attributes = await _variationThemeService.Get(request.Id);
                if (variationTheme_Attributes.Length > 0)
                {
                    response.VariationTheme_Attribute.VariationThemeId = request.Id;
                    response.VariationTheme_Attribute.VariationThemeName = variationThemeName;
                    response.VariationTheme_Attribute.Attributes = variationTheme_Attributes?.Select(p => p.ToModel()).ToArray();

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
