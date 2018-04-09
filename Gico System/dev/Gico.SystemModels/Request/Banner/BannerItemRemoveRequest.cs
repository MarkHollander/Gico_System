using Gico.Models.Request;

namespace Gico.SystemModels.Request.Banner
{
    public class BannerItemRemoveRequest : BaseRequest
    {
        public string Id { get; set; }
        public string BannerId { get; set; }
    }
}