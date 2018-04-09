using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class MenuGetRequest : BaseRequest
    {
        public string Id { get; set; }
        public EnumDefine.MenuPositionEnum Position { get; set; }
    }
}