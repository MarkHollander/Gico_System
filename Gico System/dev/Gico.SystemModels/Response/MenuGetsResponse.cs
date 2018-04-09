using Gico.Config;
using Gico.Models.Response;
using Gico.SystemDomains;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class MenuGetsResponse : BaseResponse
    {
        public MenuGetsResponse()
        {
            Positions = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.MenuPositionEnum));
            ComponentType = EnumDefine.TemplateConfigComponentTypeEnum.Banner;
        }

        public KeyValueTypeStringModel[] Languages { get; set; }

        public MenuModel[] Menus { get; set; }
        public KeyValueTypeIntModel[] Positions { get; set; }

        public string LanguageDefaultId { get; set; }
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }

    }
}