using Gico.Common;
using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Mapping
{
    public static class MenuMapping
    {
        public static MenuModel ToModel(this RMenu menu)
        {
            if (menu == null) return null;
            return new MenuModel()
            {
                Type = menu.Type.AsEnumToInt(),
                Name = menu.Name,
                Condition = menu.Condition,
                ParentId = menu.ParentId,
                Position = menu.Position,
                Url = menu.Url,
                Id = menu.Id,
                LanguageId = menu.LanguageId,
                Status = menu.Status,
                IsPublish = menu.Status == 1,
                Code = menu.Code,
                Priority = menu.Priority
            };
        }
        public static KeyValueTypeStringModel ToKeyValueModel(this RMenu menu)
        {
            if (menu == null) return null;
            return new KeyValueTypeStringModel()
            {
                Value = menu.Id,
                Checked = false,
                Text = menu.Name
            };
        }
        public static MenuAddCommand ToAddCommand(this MenuModel menu)
        {
            if (menu == null) return null;
            return new MenuAddCommand(SystemDefine.DefaultVersion)
            {
                Type = menu.Type,
                Name = menu.Name ?? string.Empty,
                Condition = menu.Condition ?? string.Empty,
                ParentId = menu.ParentId ?? string.Empty,
                Position = menu.Position,
                Url = menu.Url ?? string.Empty,
                LanguageId = menu.LanguageId ?? string.Empty,
                Status = menu.IsPublish ? 1 : 0,
                StoreId = ConfigSettingEnum.StoreId.GetConfig() ?? string.Empty,
                Priority = menu.Priority
            };
        }
        public static MenuChangeCommand ToChangeCommand(this MenuModel menu, int version)
        {
            if (menu == null) return null;
            return new MenuChangeCommand(version)
            {
                Id = menu.Id,
                Type = menu.Type,
                Name = menu.Name ?? string.Empty,
                Condition = menu.Condition ?? string.Empty,
                ParentId = menu.ParentId ?? string.Empty,
                Position = menu.Position,
                Url = menu.Url ?? string.Empty,
                LanguageId = menu.LanguageId ?? string.Empty,
                Status = menu.IsPublish ? 1 : 0,
                StoreId = ConfigSettingEnum.StoreId.GetConfig() ?? string.Empty,
                Priority = menu.Priority
            };
        }

        public static MenuBannerMappingAddCommand ToAddBannerCommand(this MenuBannerMappingAddRequest request)
        {
            if (request == null) return null;
            return new MenuBannerMappingAddCommand()
            {
                BannerId = request.BannerId,
                MenuId = request.MenuId,

            };
        }
        public static MenuBannerMappingRemoveCommand ToRemoveBannerCommand(this MenuBannerMappingRemoveRequest request)
        {
            if (request == null) return null;
            return new MenuBannerMappingRemoveCommand()
            {
                BannerId = request.BannerId,
                MenuId = request.MenuId,

            };
        }
    }
}