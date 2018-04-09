using Gico.Config;
using Gico.DataObject;
using System;

namespace Gico.MarketingDataObject
{
    public class SqlBaseDao : BaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.DbMarketingConnectionString.GetConfig();

        public class ProcName
        {
            #region Template            
            public const string Template_GetById = "Template_GetById";            
            public const string Template_Search = "Template_Search";
            public const string Template_Add = "Template_Add";
            public const string Template_Change = "Template_Change";
            public const string Template_ChangeStatus = "Template_ChangeStatus";
            #endregion

            #region Template Config            
            public const string TemplateConfig_GetById = "TemplateConfig_GetById";
            public const string TemplateConfig_GetByTemplateId = "PageTemplateConfig_GetByTemplateId";
            public const string TemplateConfig_Search = "TemplateConfig_Search";
            public const string TemplateConfig_Add = "TemplateConfig_Add";
            public const string TemplateConfig_Change = "TemplateConfig_Change";
            public const string TemplateConfig_ChangeStatus = "TemplateConfig_ChangeStatus";
            #endregion

            #region Banner            
            public const string Banner_GetById = "Banner_GetById";
            public const string Banner_GetByIds = "Banner_GetByIds";
            public const string Banner_GetByMenuId = "Banner_GetByMenuId";
            public const string Banner_Search = "Banner_Search";
            public const string Banner_Add = "Banner_Add";
            public const string Banner_Change = "Banner_Change";
            public const string Banner_ChangeStatus = "Banner_ChangeStatus";
            #endregion

            #region Banner Item            
            public const string BannerItem_GetById = "BannerItem_GetById";
            public const string BannerItem_GetByIds = "BannerItem_GetByIds";
            public const string BannerItem_Search = "BannerItem_Search";
            public const string BannerItem_Add = "BannerItem_Add";
            public const string BannerItem_Change = "BannerItem_Change";
            public const string BannerItem_ChangeStatus = "BannerItem_ChangeStatus";
            public const string BannerItem_GetByBannerId = "BannerItem_GetByBannerId";
            #endregion

            #region Menu

            public const string Menu_GetByLanguageId = "Menu_GetByLanguageId";
            public const string Menu_GetById = "Menu_GetById";
            public const string Menu_Add = "Menu_Add";
            public const string Menu_Change = "Menu_Change";
            public const string Menu_RemoveById = "Menu_RemoveById";

            #endregion

            #region Menu_Banner_Mapping

            public const string Menu_Banner_Mapping_RemoveByBannerId = "Menu_Banner_Mapping_RemoveByBannerId";
            public const string Menu_Banner_Mapping_Add = "Menu_Banner_Mapping_Add";
            public const string Menu_Banner_Mapping_Remove = "Menu_Banner_Mapping_Remove";

            #endregion

            #region ProductGroup

            public const string ProductGroup_Search = "ProductGroup_Search";
            public const string ProductGroup_Add = "ProductGroup_Add";
            public const string ProductGroup_Change = "ProductGroup_Change";
            public const string ProductGroup_ChangeConditions = "ProductGroup_ChangeConditions";
            public const string ProductGroup_Get = "ProductGroup_Get";

            #endregion
        }
    }
}
