using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class BannerGetByMenuIdRequest : BaseRequest
    {
        public string MenuId { get; set; }
    }
    
}