using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class MenuBannerMappingAddRequest : BaseRequest
    {
        public string MenuId { get; set; }
        public string BannerId { get; set; }
    }
    public class MenuBannerMappingRemoveRequest : MenuBannerMappingAddRequest
    {
    }
}