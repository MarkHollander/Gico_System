using Gico.Common;
using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;

namespace Gico.SystemAppService.Mapping
{
    public static class CategoryMapping
    {
        public static CategoryModel ToModel(this RCategory category)
        {
            if (category == null) return null;
            return new CategoryModel()
            {
                Name = category.Name,
                ParentId = category.ParentId,
                Description = category.Description,
                DisplayOrder = category.DisplayOrder,
                Id = category.Id,
                LanguageId = category.LanguageId,
                Status = category.Status,
                Code = category.Code,
                Logos = category.Logos


            };
        }
        public static CategoryAttrModel ToModel(this RCategoryAttr categoryAttr)
        {
            if (categoryAttr == null) return null;
            return new CategoryAttrModel()
            {

                AttributeId = categoryAttr.AttributeId,
                AttributeName = categoryAttr.AttributeName,
                AttributeType = categoryAttr.AttributeType,
                BaseUnitId = categoryAttr.BaseUnitId,
                DisplayOrder = categoryAttr.DisplayOrder,
                IsFilter = categoryAttr.IsFilter

            };
        }

        public static KeyValueTypeStringModel ToKeyValueModel(this RCategory category)
        {
            if (category == null) return null;
            return new KeyValueTypeStringModel()
            {
                Value = category.Id,
                Checked = false,
                Text = category.Name
            };
        }
        public static KeyValueTypeStringModel ToKeyValueModel(this RCategory category, bool @checked)
        {
            if (category == null) return null;
            return new KeyValueTypeStringModel()
            {
                Value = category.Id,
                Checked = @checked,
                Text = category.Name
            };
        }

        public static JsTreeModel ToJstreeStateModel(this RCategory category, bool opened, bool selected, bool disabled)
        {
            if (category == null) return null;
            return new JsTreeModel()
            {
                Id = category.Id,
                Text = category.Name,
                Parent = string.IsNullOrEmpty(category.ParentId) ? "#" : category.ParentId,
                State = new JstreeStateModel()
                {
                    Disabled = disabled,
                    Opened = opened,
                    Selected = selected
                }
            };
        }

        public static CategoryAddCommand ToAddCommand(this CategoryModel category, string userId, string code)
        {
            if (category == null) return null;
            return new CategoryAddCommand(SystemDefine.DefaultVersion)
            {

                Description = category.Description ?? string.Empty,
                CreatedUid = userId,
                DisplayOrder = category.DisplayOrder,
                Status = category.IsPublish ? 1 : 0,
                LanguageId = category.LanguageId ?? string.Empty,
                StoreId = ConfigSettingEnum.StoreId.GetConfig() ?? string.Empty,
                Name = category.Name ?? string.Empty,
                ParentId = category.ParentId ?? string.Empty,
                Code = code
            };
        }

        public static CategoryChangeCommand ToChangeCommand(this CategoryModel category, int version)
        {
            if (category == null) return null;
            return new CategoryChangeCommand(version)
            {
                Id = category.Id,
                Description = category.Description ?? string.Empty,
                StoreId = ConfigSettingEnum.StoreId.GetConfig() ?? string.Empty,
                DisplayOrder = category.DisplayOrder,
                Status = category.IsPublish ? 1 : 0,
                LanguageId = category.LanguageId ?? string.Empty,
                Name = category.Name ?? string.Empty,
                ParentId = category.ParentId ?? string.Empty,
            };
        }


    }
}
