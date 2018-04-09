using Gico.Config;
using Gico.Models.Response;
using Gico.SystemDomains;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class MenuGetResponse : BaseResponse
    {
        public MenuGetResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.MenuTypeEnum), false);
            Positions = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.MenuPositionEnum), false);
        }
        public MenuModel[] Parents { get; set; }
        public KeyValueTypeIntModel[] Types { get; private set; }
        public KeyValueTypeIntModel[] Positions { get; private set; }

        public KeyValueTypeStringModel[] Languages { get; set; }

        public MenuModel Menu { get; set; }

    }
}