using Gico.FrontEndModels;
using Gico.FrontEndModels.Models;
using Gico.ReadSystemModels;

namespace Gico.FrontEndAppService.Mapping
{
    public static class MenuMapping
    {
        public static MenuModel ToModel(this RMenu menu)
        {
            if (menu == null) return null;
            return new MenuModel()
            {
                Name = menu.Name,
                Url = menu.Url
            };
        }
    }
}
