using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Mapping;
using Gico.SystemDomains;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gico.SystemAppService.Implements
{
    public class LocaleStringResourceAppService : ILocaleStringResourceAppService
    {
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly ILanguageService _languageService;
        private readonly ICurrentContext _context;
        private readonly ICommonService _commonService;
        private readonly ILogger<LocaleStringResourceAppService> _logger;

        public LocaleStringResourceAppService(ILocaleStringResourceService localeStringResourceService, ILanguageService languageService,ICurrentContext context, ILogger<LocaleStringResourceAppService> logger, ICommonService commonService)
        {
            _localeStringResourceService = localeStringResourceService;
            _languageService = languageService;
            _context = context;
            _commonService = commonService;
            _logger = logger;
        }


        public async Task<BaseResponse> Add(LocaleStringResourceAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var command = request.ToCommand();
                CommandResult result = await _localeStringResourceService.SendCommand(command);
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

        public async Task<BaseResponse> Change(LocaleStringResourceChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var command = request.ToCommand();
                var result = await _localeStringResourceService.SendCommand(command);
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

        public async Task<LocaleStringResourceSearchResponse> Search(LocaleStringResourceSearchRequest request)
        {
            LocaleStringResourceSearchResponse response = new LocaleStringResourceSearchResponse();
            
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, 30);
                try
                {
                    RefSqlPaging sqlpaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                    var data = await _localeStringResourceService.Search(request.LanguageId, request.ResourceName, request.ResourceValue, sqlpaging);
                    response.TotalRow = paging.TotalRow;
                    response.Locales = data.Select(p => p.ToModel()).ToArray();
                    response.PageIndex = request.PageIndex;
                    response.PageSize = request.PageSize;

                RLanguage[] languages = await _languageService.Get();
                if (languages.Length > 0)
                {
                    response.Languages = languages.Select(p => p.ToKeyValueModel()).ToArray();
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

        public async Task<LocaleStringResourceGetResponse> Get(LocaleStringResourceGetRequest request)
        {
            LocaleStringResourceGetResponse response = new LocaleStringResourceGetResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    RLocaleStringResource locale = await _localeStringResourceService.GetById(request.Id);
                    if (locale == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.LocaleStringResourceNotFound);
                        return response;
                    }
                    response.Locale = locale.ToModel();
                }
                else
                {
                    response.Locale = new LocaleStringResourceViewModel()
                    {
                        Status = false,
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
    }
}
