using Gico.Common;
using Gico.Config;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;

namespace Gico.SystemAppService.Mapping
{
    public static class AttrCategoryMapping
    {
        public static AttrCategoryAddCommand ToAddCommand(this AttrCategoryModel attrCategory)
        {
            if (attrCategory == null) return null;
            return new AttrCategoryAddCommand()
            {
                AttributeId = attrCategory.AttributeId,
                CategoryId = attrCategory.CategoryId,
                AttributeType = attrCategory.AttributeType,
                DisplayOrder = attrCategory.DisplayOrder,
                BaseUnitId = attrCategory.BaseUnitId,
                IsRequired = attrCategory.IsRequired,
                IsFilter = attrCategory.IsFilter,
                FilterSpan = attrCategory.FilterSpan ?? ""


            };
        }

        public static AttrCategoryChangeCommand ToChangeCommand(this AttrCategoryModel attrCategory)
        {
            if (attrCategory == null) return null;
            return new AttrCategoryChangeCommand()
            {
                AttributeId = attrCategory.AttributeId,
                CategoryId = attrCategory.CategoryId,
                AttributeType = attrCategory.AttributeType,
                DisplayOrder = attrCategory.DisplayOrder,
                BaseUnitId = attrCategory.BaseUnitId,
                IsRequired = attrCategory.IsRequired,
                IsFilter = attrCategory.IsFilter,
                FilterSpan = attrCategory.FilterSpan ?? ""
            };
        }

        public static AttrCategoryRemoveCommand ToRemoveCommand(this AttrCategoryModel attrCategory)
        {
            if (attrCategory == null) return null;
            return new AttrCategoryRemoveCommand()
            {
                AttributeId = attrCategory.AttributeId,
                CategoryId = attrCategory.CategoryId,
               
            };
        }

        //public static KeyValueTypeStringModel ToKeyValueModel(this RCategory category)
        //{
        //    if (category == null) return null;
        //    return new KeyValueTypeStringModel()
        //    {
        //        Value = category.Id,
        //        Checked = false,
        //        Text = category.Name
        //    };
        //}

        public static AttrCategoryModel ToModel(this RAttrCategory attrCategory)
        {
            if (attrCategory == null) return null;
            return new AttrCategoryModel()
            {
                AttributeId = attrCategory.AttributeId,
                AttributeType = attrCategory.AttributeType,
                BaseUnitId = attrCategory.BaseUnitId,
                DisplayOrder = attrCategory.DisplayOrder,
                FilterSpan  =attrCategory.FilterSpan,
                IsFilter = attrCategory.IsFilter,
                IsRequired = attrCategory.IsRequired,
                CategoryId = attrCategory.CategoryId,
                AttributeName = attrCategory.AttributeName


            };
        }

    

    }
}
