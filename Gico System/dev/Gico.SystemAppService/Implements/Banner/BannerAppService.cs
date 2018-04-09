using Gico.AppService;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.SystemAppService.Interfaces.Banner;
using Gico.SystemModels.Request.Banner;
using Gico.SystemModels.Response.Banner;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.Banner;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using Gico.SystemAppService.Mapping.Banner;
using Gico.Common;
using Gico.Models.Response;
using Gico.SystemModels.Models.Banner;

namespace Gico.SystemAppService.Implements.Banner
{
    public class BannerAppService : IBannerAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<BannerAppService> _logger;
        private readonly IBannerService _bannerService;
        private readonly ICommonService _commonService;

        public BannerAppService(ILogger<BannerAppService> logger, IBannerService bannerService, ICommonService commonService, ICurrentContext context)
        {
            _logger = logger;
            _bannerService = bannerService;
            _commonService = commonService;
            _context = context;
        }

        public async Task<BannerSearchResponse> SearchBanner(BannerSearchRequest request)
        {
            BannerSearchResponse response = new BannerSearchResponse();
            try
            {
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);

                var data = await _bannerService.SearchBanner(request.Id, request.BannerName, request.Status, paging);
                response.TotalRow = paging.TotalRow;
                response.Banners = data.Select(p => p.ToModel()).ToArray();
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

        public async Task<BannerGetResponse> GetBannerById(BannerGetRequest request)
        {
            var response = new BannerGetResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                {
                    response.Banner = new BannerViewModel()
                    {
                        Status = EnumDefine.CommonStatusEnum.New
                    };
                }
                else
                {
                    var data = await _bannerService.GetBannerById(request.Id);
                    if (data == null)
                    {
                        response.SetFail("Banner is null.");
                        return response;

                    }
                    response.Banner = data.ToModel();
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

        public async Task<BannerAddOrChangeResponse> BannerAddOrChange(BannerAddOrChangeRequest request)
        {
            var response = new BannerAddOrChangeResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                if (string.IsNullOrEmpty(request.Id))
                {
                    //add
                    request.Status = EnumDefine.CommonStatusEnum.New;
                    var command = request.ToCommandAdd(userLogin.Id);
                    var result = await _bannerService.SendCommand(command);
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
                    var data = await _bannerService.GetBannerById(request.Id);
                    if (data == null || string.IsNullOrEmpty(data.Id))
                    {
                        response.SetFail("Banner not found!");
                        return response;
                    }
                    //update
                    var command = request.ToCommandChange(userLogin.Id);
                    var result = await _bannerService.SendCommand(command);
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

        public async Task<BaseResponse> BannerRemove(BannerRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                var command = request.ToCommandRemove(userLogin.Id);
                var result = await _bannerService.SendCommand(command);
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

        public async Task<BannerItemSearchResponse> SearchBannerItem(BannerItemSearchRequest request)
        {
            BannerItemSearchResponse response = new BannerItemSearchResponse();
            try
            {
                var banner = await _bannerService.GetBannerById(request.BannerId);
                if (banner == null)
                {
                    response.SetFail("Banner not found!!!");
                    return response;
                }
                RefSqlPaging paging = new RefSqlPaging(request.PageIndex, request.PageSize);
                DateTimeRange from = new DateTimeRange(request.FromStartDate?.AsDateTimeNullable(SystemDefine.DateFormat), request.ToStartDate?.AsDateTimeNullable(SystemDefine.DateFormat));
                DateTimeRange to = new DateTimeRange(request.FromEndDate?.AsDateTimeNullable(SystemDefine.DateFormat), request.ToEndDate?.AsDateTimeNullable(SystemDefine.DateFormat));

                var data = await _bannerService.SearchBannerItem(request.Id, request.BannerItemName, request.BannerId, request.Status, request.IsDefault, from, to, paging);
                response.TotalRow = paging.TotalRow;
                response.BannerItems = data.Select(p => p.ToModel()).ToArray();
                response.Banner = banner.ToModel();
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

        public async Task<BannerItemGetResponse> GetBannerItemById(BannerItemGetRequest request)
        {
            var response = new BannerItemGetResponse();
            try
            {
                var banner = await _bannerService.GetBannerById(request.BannerId);
                if (banner == null)
                {
                    response.SetFail("Banner not found.");
                    return response;
                }
                response.Banner = banner.ToModel();
                if (string.IsNullOrEmpty(request.Id))
                {
                    response.BannerItem = new BannerItemViewModel()
                    {
                        Status = EnumDefine.CommonStatusEnum.New,
                        BannerId = request.BannerId
                    };
                }
                else
                {
                    var data = await _bannerService.GetBannerItemById(request.Id);
                    response.BannerItem = data.ToModel();
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

        public async Task<BannerItemAddOrChangeResponse> BannerItemAddOrChange(BannerItemAddOrChangeRequest request)
        {
            var response = new BannerItemAddOrChangeResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                if (string.IsNullOrEmpty(request.Id))
                {
                    //add
                    var command = request.ToCommandAdd(userLogin.Id);
                    var result = await _bannerService.SendCommand(command);
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
                    var data = await _bannerService.GetBannerItemById(request.Id);
                    if (data == null)
                    {
                        response.SetFail("Template not found!");
                        return response;
                    }
                    //update
                    var command = request.ToCommandChange(userLogin.Id);
                    var result = await _bannerService.SendCommand(command);
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

        public async Task<BaseResponse> BannerItemRemove(BannerItemRemoveRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                var command = request.ToCommandRemove(userLogin.Id);
                var result = await _bannerService.SendCommand(command);
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
