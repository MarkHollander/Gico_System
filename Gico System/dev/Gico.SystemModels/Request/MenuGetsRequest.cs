using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class MenuGetsRequest : BaseRequest
    {
        public EnumDefine.MenuPositionEnum Position { get; set; }
    }
}