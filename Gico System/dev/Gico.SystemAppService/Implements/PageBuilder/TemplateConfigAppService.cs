using Gico.AppService;
using Gico.Config;
using Gico.SystemModels.Request.PageBuilder;
using Gico.SystemModels.Response.PageBuilder;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.PageBuilder;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using Gico.SystemAppService.Mapping.PageBuilder;
using Gico.CQRS.Model.Implements;
using Gico.Models.Response;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemAppService.Interfaces.PageBuilder;
using Gico.SystemAppService.Mapping.Banner;
using Gico.SystemModels.Models.PageBuilder;
using Gico.SystemService.Interfaces.Banner;

namespace Gico.SystemAppService.Implements.PageBuilder
{
    public class TemplateConfigAppService : ITemplateConfigAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<TemplateConfigAppService> _logger;
        private readonly ITemplateService _templateService;
        private readonly ICommonService _commonService;
        private readonly IBannerService _bannerService;


        public TemplateConfigAppService(ILogger<TemplateConfigAppService> logger, ICommonService commonService, ICurrentContext context, ITemplateService templateService, IBannerService bannerService)
        {
            _logger = logger;
            _templateService = templateService;
            _bannerService = bannerService;
            _commonService = commonService;
            _context = context;
        }

        public async Task<TemplateConfigSearchResponse> Search(TemplateConfigSearchRequest request)
        {
            TemplateConfigSearchResponse response = new TemplateConfigSearchResponse();
            try
            {
                if (string.IsNullOrEmpty(request.TemplateId))
                {
                    response.SetFail("Template not found!!!");
                    return response;
                }

                var template = await _templateService.GetById(request.TemplateId);
                if (template == null)
                {
                    response.SetFail("Template not found!!!");
                    return response;
                }
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _templateService.SearchTemplateConfig(request.Id, request.TemplateId, request.ComponentType, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.TemplateConfigs = data.Select(p => p.ToModel()).ToArray();
                response.Template = template.ToModel();
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

        public async Task<TemplateConfigGetResponse> GetTemplateConfigById(TemplateConfigGetRequest request)
        {
            var response = new TemplateConfigGetResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    response.TemplateConfig = new TemplateConfigViewModel()
                    {
                        Status = EnumDefine.CommonStatusEnum.New,
                        TemplateId = request.TemplateId,
                    };
                }
                else
                {
                    var data = await _templateService.GetTemplateConfigById(request.Id);
                    if (data == null)
                    {
                        response.SetFail("TemplateConfig is null");
                        return response;
                    }
                    KeyValueTypeStringModel componentModel = null;
                    if (!string.IsNullOrEmpty(data.ComponentId))
                    {
                        switch (data.ComponentType)
                        {
                            case EnumDefine.TemplateConfigComponentTypeEnum.Banner:
                                var component = await _bannerService.GetBannerById(data.ComponentId);
                                if (component == null)
                                {
                                    break;
                                }
                                componentModel = component.ToAutocompleteModel();
                                break;
                            case EnumDefine.TemplateConfigComponentTypeEnum.Menu:
                                break;
                            case EnumDefine.TemplateConfigComponentTypeEnum.ProductGroup:
                                break;
                        }
                    }

                    response.TemplateConfig = data.ToModel(componentModel);
                }
                if (response.TemplateConfig != null && !string.IsNullOrEmpty(response.TemplateConfig.TemplateId))
                {
                    var template = await _templateService.GetById(response.TemplateConfig.TemplateId);

                    if (template != null && !string.IsNullOrEmpty(template.Id))
                    {
                        response.Template = template.ToModel();
                    }
                }
                else
                {
                    var template = await _templateService.GetById(request.TemplateId);
                    if (template != null && !string.IsNullOrEmpty(template.Id))
                    {
                        response.Template = template.ToModel();
                    }
                    else
                    {
                        response.SetFail("Template not found!!!");
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

        public async Task<TemplateConfigAddOrChangeResponse> TemplateConfigAddOrChange(TemplateConfigAddOrChangeRequest request)
        {
            var response = new TemplateConfigAddOrChangeResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();

                if (string.IsNullOrEmpty(request.Id))
                {
                    //add
                    var command = request.ToCommandAdd(userLogin.Id);
                    var result = await _templateService.SendCommand(command);
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
                    var data = await _templateService.GetTemplateConfigById(request.Id);
                    if (data == null || string.IsNullOrEmpty(data.Id))
                    {
                        response.SetFail("Template config not found!");
                        return response;
                    }
                    //update
                    var command = request.ToCommandChange(userLogin.Id);
                    var result = await _templateService.SendCommand(command);
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

        public async Task<TemplateCheckCodeExistResponse> TemplateConfigCheckCodeExist(
            TemplateCheckCodeExistRequest request)
        {
            TemplateCheckCodeExistResponse response = new TemplateCheckCodeExistResponse();
            try
            {
                RTemplateConfig[] templateConfigs = await _templateService.GetTemplateConfigByTemplateId(request.TemplateId);
                if (!string.IsNullOrEmpty(request.CurrentTemplateConfigId))
                {
                    templateConfigs = templateConfigs.Where(p => p.Id != request.CurrentTemplateConfigId).ToArray();
                }
                response.IsExist = templateConfigs != null && templateConfigs.Length > 0 &&
                                   templateConfigs.Any(p => p.TemplatePositionCode == request.Code);
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ComponentsAutocompleteResponse> ComponentsAutocomplete(ComponentsAutocompleteRequest request)
        {
            ComponentsAutocompleteResponse response = new ComponentsAutocompleteResponse();
            try
            {
                RefSqlPaging sqlPaging = new RefSqlPaging(0, 100);
                switch (request.ComponentType)
                {
                    case EnumDefine.TemplateConfigComponentTypeEnum.Banner:
                        {
                            var banners = await _bannerService.SearchBanner(string.Empty, request.Tearm, 0, sqlPaging);
                            response.Components = banners.Select(p => p.ToAutocompleteModel()).ToArray();
                        }
                        break;
                    case EnumDefine.TemplateConfigComponentTypeEnum.Menu:
                        {

                        }
                        break;
                    case EnumDefine.TemplateConfigComponentTypeEnum.ProductGroup:
                        break;
                }
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> TemplateConfigRemove(TemplateConfigRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                var command = request.ToCommandRemove(userLogin.Id);
                var result = await _templateService.SendCommand(command);
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
