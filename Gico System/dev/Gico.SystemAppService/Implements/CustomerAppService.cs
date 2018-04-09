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
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<CustomerAppService> _logger;
        private readonly ICustomerService _customerService;
        private readonly ICommonService _commonService;
        private readonly ILanguageService _languageService;

        public CustomerAppService(ILogger<CustomerAppService> logger, ICustomerService customerService, ICommonService commonService, ILanguageService languageService, ICurrentContext context)
        {
            _logger = logger;
            _customerService = customerService;
            _commonService = commonService;
            _languageService = languageService;
            _context = context;
        }


        public async Task<CustomerSearchResponse> Search(CustomerSearchRequest request)
        {
            CustomerSearchResponse response = new CustomerSearchResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _customerService.Search(request.Code, request.Email, request.Mobile, request.FullName,
                     request.FromBirthdayValue, request.ToBirthDayValue, request.Type, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.Customers = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<CustomerAddOrChangeResponse> AddOrChange(CustomerAddOrChangeRequest request)
        {
            CustomerAddOrChangeResponse response = new CustomerAddOrChangeResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                CommandResult result;
                if (string.IsNullOrEmpty(request.Id))
                {
                    long systemIdentity = await _commonService.GetNextId(typeof(Customer));
                    string code = Common.Common.GenerateCodeFromId(systemIdentity, 3);
                    var command = request.ToCommand(_context.Ip, userLogin.Id, code);
                    result = await _customerService.SendCommand(command);
                }
                else
                {
                    var command = request.ToCommand(_context.Ip, userLogin.Id, request.Version);
                    result = await _customerService.SendCommand(command);
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

        public async Task<CustomerGetResponse> Get(CustomerGetRequest request)
        {
            CustomerGetResponse response = new CustomerGetResponse();
            var userLogin = await _context.GetCurrentCustomer();
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    var customer = await _customerService.GetFromDb(request.Id);
                    if (customer == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                        return response;
                    }
                    response.Customer = customer.ToModel();
                    foreach (var type in response.Types)
                    {
                        type.Checked = response.Customer.Type.HasFlag((EnumDefine.CustomerTypeEnum)type.Id);
                    }
                }
                else
                {
                    response.Customer = new CustomerViewModel()
                    {
                        LanguageId = userLogin.CurrentLanguageId,
                        TwoFactorEnabled = EnumDefine.TwoFactorEnum.Disable,
                        Gender = EnumDefine.GenderEnum.Male,
                        Status = EnumDefine.CustomerStatusEnum.New,
                        Type = EnumDefine.CustomerTypeEnum.IsCustomer,
                        Id = string.Empty,
                        PhoneNumber = string.Empty,
                        Email = string.Empty,
                        Password = string.Empty
                    };
                }
                var langguages = await _languageService.Get();
                response.Languages = langguages?.Select(p => p.ToKeyValueModel()).ToArray();
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