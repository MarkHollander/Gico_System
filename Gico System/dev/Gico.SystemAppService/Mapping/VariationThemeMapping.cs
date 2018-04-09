using Gico.Common;
using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;

namespace Gico.SystemAppService.Mapping
{
    public static class VariationThemeMapping
    {
        public static VariationThemeModel ToModel(this RVariationTheme variationTheme)
        {
            if (variationTheme == null) return null;
            return new VariationThemeModel()
            {

                VariationThemeId = variationTheme.VariationThemeId,
                Status = variationTheme.VariationThemeStatus,
                VariationThemeName = variationTheme.VariationThemeName

            };
        }

        public static CategoryVariationThemeMappingModel ToModel(this RCategory_VariationTheme_Mapping rCategory_VariationTheme_Mapping)
        {
            if (rCategory_VariationTheme_Mapping == null) return null;
            return new CategoryVariationThemeMappingModel()
            {

                VariationThemeId = rCategory_VariationTheme_Mapping.VariationThemeId,
                CategoryId = rCategory_VariationTheme_Mapping.CategoryId,
                VariationThemeName = rCategory_VariationTheme_Mapping.VariationThemeName

            };
        }

        public static KeyValueTypeIntModel ToKeyValueModel(this RVariationTheme variationTheme)
        {
            if (variationTheme == null) return null;
            return new KeyValueTypeIntModel()
            {
                Value = variationTheme.VariationThemeId,
                Checked = false,
                Text = variationTheme.VariationThemeName
            };
        }


        public static ProductAttributeModel ToModel(this RVariationTheme_Attribute variationTheme_Attribute)
        {
            if (variationTheme_Attribute == null) return null;
            return new ProductAttributeModel
            {
                Id = variationTheme_Attribute.AttributeId,
                Name = variationTheme_Attribute.AttributeName

            };
        }

        public static Category_VariationTheme_Mapping_AddCommand ToAddCommand(this Category_VariationTheme_MappingModel category_VariationTheme_Mapping)
        {
            if (category_VariationTheme_Mapping == null) return null;
            return new Category_VariationTheme_Mapping_AddCommand()
            {

                CategoryId = category_VariationTheme_Mapping.CategoryId,
                VariationThemeId = category_VariationTheme_Mapping.VariationThemeId
            };
        }

        public static Category_VariationTheme_Mapping_RemoveCommand ToRemoveCommand(this CategoryVariationThemeMappingModel category_VariationTheme_Mapping)
        {
            if (category_VariationTheme_Mapping == null) return null;
            return new Category_VariationTheme_Mapping_RemoveCommand()
            {

                CategoryId = category_VariationTheme_Mapping.CategoryId,
                VariationThemeId = category_VariationTheme_Mapping.VariationThemeId
            };
        }


    }
}
