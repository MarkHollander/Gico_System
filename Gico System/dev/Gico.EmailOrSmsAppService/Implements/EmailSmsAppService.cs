using Gico.AppService;
using Gico.Config;
using Gico.EmailOrSmsAppService.Interfaces;
using Gico.EmailOrSmsModel.Mapping;
using Gico.EmailOrSmsModel.Model;
using Gico.EmailOrSmsModel.Request;
using Gico.EmailOrSmsModel.Response;
using Gico.EmailOrSmsService.Interfaces;
using Gico.Models.Response;
using Gico.ReadEmailSmsModels;
using Gico.SystemModels.Request;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gico.EmailOrSmsAppService.Implements
{
    public class EmailSmsAppService : IEmailSmsAppService
    {
        private readonly IEmailSmsService _emailSmsService;
        private readonly ICurrentContext _context;
        private readonly ICommonService _commonService;
        private readonly ILogger<EmailSmsAppService> _logger;

        public EmailSmsAppService(IEmailSmsService emailSmsService, ICurrentContext context, ICommonService commonService, ILogger<EmailSmsAppService> logger)
        {
            _emailSmsService = emailSmsService;
            _context = context;
            _commonService = commonService;
            _logger = logger;
        }

        public async Task<EmailOrSmsSearchResponse> Search(EmailOrSmsSearchRequest request)
        {
            EmailOrSmsSearchResponse response = new EmailOrSmsSearchResponse();
            RefSqlPaging paging = new RefSqlPaging(request.PageIndex, 30);
            try
            {
                RefSqlPaging sqlpaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _emailSmsService.Search(request.Type, request.MessageType, request.PhoneNumber, request.Email, request.Status, request.CreatedDateUtc, request.SendDate, sqlpaging);
                response.TotalRow = paging.TotalRow;
                response.EmailSmses = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<EmailOrSmsGetResponse> GetDetail(EmailOrSmsGetRequest request)
        {
            EmailOrSmsGetResponse response = new EmailOrSmsGetResponse();
            try
            {
                if(!string.IsNullOrEmpty(request.Id))
                {
                    var emailSms = await _emailSmsService.GetFromDb(request.Id);
                    if(emailSms == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.EmailNotFound);
                        return response;
                    }
                    response.EmailSms = emailSms.ToModel();
                    foreach (var messageType in response.MessageTypes)
                    {
                        messageType.Checked = response.EmailSms.MessageType.HasFlag((EnumDefine.EmailOrSmsMessageTypeEnum)messageType.Id);
                    }
                }
                
            }
            catch(Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<VerifyGetResponse> GetVerifyDetail(VerifyGetRequest request)
        {
            VerifyGetResponse response = new VerifyGetResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    RVerify verify = await _emailSmsService.GetVerifyFromDb(request.Id);
                    if (verify == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.VerifyNotFound);
                        return response;
                    }
                    response.Verify = verify.ToModel();
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

