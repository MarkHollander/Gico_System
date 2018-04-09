using System;
using System.Linq;
using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.AppService;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Response;
using Microsoft.Extensions.Logging;
using Gico.Models.Response;
using Gico.SystemAppService.Mapping;
using Gico.SystemDomains;
using Gico.SystemModels;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;
using Gico.SystemService.Interfaces;
using Polly.Retry;

namespace Gico.SystemAppService.Implements
{
    public class LanguageAppService : ILanguageAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<LanguageAppService> _logger;
        private readonly ICommonService _commonService;
        private readonly ILanguageService _languageService;

        public LanguageAppService(ILogger<LanguageAppService> logger, ICommonService commonService, ILanguageService languageService, ICurrentContext context)
        {
            _logger = logger;
            _commonService = commonService;
            _languageService = languageService;
            _context = context;
        }

        public async Task<LanguageSearchResponse> Search(LanguageSearchRequest request)
        {
            LanguageSearchResponse response = new LanguageSearchResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _languageService.Search(request.Name, paging);
                response.TotalRow = paging.TotalRow;
                response.Languages = data.Select(p => p.ToModel()).ToArray();
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


        public async Task<LanguageAddOrChangeResponse> AddOrChange(LanguageAddOrChangeRequest request)
        {
            LanguageAddOrChangeResponse response = new LanguageAddOrChangeResponse();
            try
            {
                
                CommandResult result;
                if (string.IsNullOrEmpty(request.Id.AsString()))
                {
                    
                    var command = request.ToCommand(_context.Ip);
                    result = await _languageService.SendCommand(command);
                }
                else
                {
                    var command = request.ToCommand(_context.Ip, request.Version);
                    result = await _languageService.SendCommand(command);
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


    }
}