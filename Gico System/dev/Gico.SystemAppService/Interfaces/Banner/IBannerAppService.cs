using Gico.SystemModels.Request.Banner;
using Gico.SystemModels.Response.Banner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Models.Response;

namespace Gico.SystemAppService.Interfaces.Banner
{
    public interface IBannerAppService
    {
        Task<BannerSearchResponse> SearchBanner(BannerSearchRequest request);
        Task<BannerGetResponse> GetBannerById(BannerGetRequest request);
        Task<BannerAddOrChangeResponse> BannerAddOrChange(BannerAddOrChangeRequest request);
        Task<BaseResponse> BannerRemove(BannerRemoveRequest request);
        Task<BannerItemSearchResponse> SearchBannerItem(BannerItemSearchRequest request);
        Task<BannerItemGetResponse> GetBannerItemById(BannerItemGetRequest request);
        Task<BannerItemAddOrChangeResponse> BannerItemAddOrChange(BannerItemAddOrChangeRequest request);
        Task<BaseResponse> BannerItemRemove(BannerItemRemoveRequest request);
    }
}
