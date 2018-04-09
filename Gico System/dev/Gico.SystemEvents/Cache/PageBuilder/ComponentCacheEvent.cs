using System;
using Gico.Config;
using Gico.Events;

namespace Gico.SystemEvents.Cache.PageBuilder
{
    public class ComponentCacheEvent : BaseEvent
    {
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }
        private string _componentSetting;

        public object ComponentSetting
        {
            get
            {
                switch (ComponentType)
                {
                    case EnumDefine.TemplateConfigComponentTypeEnum.Banner:
                        return Common.Serialize.JsonDeserializeObject<BannerCacheEvent>(_componentSetting);
                }
                return null;
            }
            set => _componentSetting = Common.Serialize.JsonSerializeObject(value);
        }

    }

    public class BannerCacheEvent : BaseEvent
    {
        #region Properties
        public string BannerName { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public string BackgroundRgb { get; set; }
        public BannerItemCacheEvent[] BannerItems { get; set; }
        #endregion
    }

    public class BannerItemCacheEvent : BaseEvent
    {
        #region Properties
        public string BannerItemName { get; set; }
        public string BannerId { get; set; }
        public string TargetUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public bool IsDefault { get; set; }
        public string BackgroundRgb { get; set; }
        #endregion
    }

    public class BannerRemoveEvent : BaseEvent
    {

    }
}