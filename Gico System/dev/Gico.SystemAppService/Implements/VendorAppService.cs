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
    public class VendorAppService : IVendorAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<VendorAppService> _logger;
        private readonly IVendorService _vendorService;
        private readonly ICommonService _commonService;

        public VendorAppService(ILogger<VendorAppService> logger, IVendorService vendorService, ICommonService commonService, ICurrentContext context)
        {
            _logger = logger;
            _vendorService = vendorService;
            _commonService = commonService;
            _context = context;
        }


        public async Task<VendorSearchResponse> Search(VendorSearchRequest request)
        {
            VendorSearchResponse response = new VendorSearchResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                var data = await _vendorService.Search(request.Code, request.Email, request.Phone, request.Name, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.Vendors = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<VendorAddOrChangeResponse> AddOrChange(VendorAddOrChangeRequest request)
        {
            VendorAddOrChangeResponse response = new VendorAddOrChangeResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                CommandResult result;
                if (string.IsNullOrEmpty(request.Id))
                {
                    long systemIdentity = await _commonService.GetNextId(typeof(Vendor));
                    string code = Common.Common.GenerateCodeFromId(systemIdentity, 3);
                    var command = request.ToCommand(_context.Ip, userLogin.Id, code);
                    result = await _vendorService.SendCommand(command);
                }
                else
                {
                    var command = request.ToCommand(_context.Ip, userLogin.Id, request.Version);
                    result = await _vendorService.SendCommand(command);
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

        public async Task<VendorGetResponse> Get(VendorGetRequest request)
        {
            VendorGetResponse response = new VendorGetResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    var vendor = await _vendorService.GetFromDb(request.Id);
                    if (vendor == null)
                    {
                        response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                        return response;
                    }
                    response.Vendor = vendor.ToModel();
                    foreach (var type in response.Types)
                    {
                        type.Checked = response.Vendor.Type.HasFlag((EnumDefine.VendorTypeEnum)type.Id);
                    }
                }
                else
                {
                    response.Vendor = new VendorViewModel()
                    {

                        Status = EnumDefine.VendorStatusEnum.New,
                        Type = EnumDefine.VendorTypeEnum.IsA,
                        Id = string.Empty,
                        Phone = string.Empty,
                        Email = string.Empty
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