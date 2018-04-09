using System;
using Gico.Config;
using Gico.Models.Models;
namespace Gico.SystemModels.Models
{
    public class VariationThemeModel : BaseModel
    {
        public int VariationThemeId { get; set; }
        public string VariationThemeName { get; set; }
        public new EnumDefine.VariationThemeStatus Status { get; set; }
    }

    public class VariationTheme_AttributeModel : BaseModel
    {
        public int VariationThemeId { get; set; }
        public string VariationThemeName { get; set; }

        public ProductAttributeModel[] Attributes { get; set; }

    }
    public class Category_VariationTheme_MappingModel : BaseModel
    {
        public int[] VariationThemeId { get; set; }
        public string CategoryId { get; set; }

    }

    public class CategoryVariationThemeMappingModel : BaseModel
    {
        public int VariationThemeId { get; set; }
        public string CategoryId { get; set; }

        public string VariationThemeName { get; set; }

    }

}
