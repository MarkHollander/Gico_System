using Gico.Models.Response;
using Gico.SystemModels.Models.Banner;

namespace Gico.SystemModels.Response
{
    public class BannersGetByMenuIdResponse : BaseResponse
    {
        public BannerViewModel[] Banners { get; set; }
    }

}