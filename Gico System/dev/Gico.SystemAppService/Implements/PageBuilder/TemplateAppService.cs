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
using Gico.Common;
using Gico.SystemAppService.Mapping.PageBuilder;
using Gico.CQRS.Model.Implements;
using Gico.Models.Response;
using Gico.SystemAppService.Interfaces.PageBuilder;
using Gico.SystemModels.Models.PageBuilder;

namespace Gico.SystemAppService.Implements.PageBuilder
{
    public class TemplateAppService : ITemplateAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<TemplateAppService> _logger;
        private readonly ITemplateService _templateService;
        private readonly ICommonService _commonService;

        public TemplateAppService(ILogger<TemplateAppService> logger, ITemplateService templateService, ICommonService commonService, ICurrentContext context)
        {
            _logger = logger;
            _templateService = templateService;
            _commonService = commonService;
            _context = context;
        }

        public async Task<TemplateSearchResponse> Search(TemplateSearchRequest request)
        {
            request.PageSize = ConfigSettingEnum.PageSizeDefault.GetConfig().AsInt(30);
            TemplateSearchResponse response = new TemplateSearchResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _templateService.Search(request.Code, request.TemplateName, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.Templates = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<TemplateGetResponse> GetTemplateById(TemplateGetRequest request)
        {
            var response = new TemplateGetResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    response.Template = new TemplateViewModel()
                    {
                        Status = EnumDefine.CommonStatusEnum.New
                    };
                }
                else
                {
                    var data = await _templateService.GetById(request.Id);
                    response.Template = data.ToModel();
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

        public async Task<TemplateAddOrChangeResponse> TemplateAddOrChange(TemplateAddOrChangeRequest request)
        {
            var response = new TemplateAddOrChangeResponse();

            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                if (string.IsNullOrEmpty(request.Id))
                {
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
                    var data = await _templateService.GetById(request.Id);
                    if (data == null || string.IsNullOrEmpty(data.Id))
                    {
                        response.SetFail("Template not found!");
                        return response;
                    }
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

        public async Task<BaseResponse> TemplateRemove(TemplateRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                var command = request.ToRemoveCommand(userLogin.Id);
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
