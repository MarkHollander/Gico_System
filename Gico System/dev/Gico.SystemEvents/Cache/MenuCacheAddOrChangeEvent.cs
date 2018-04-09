using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemEvents.Cache
{
    public class MenuCacheAddOrChangeEvent : Event
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public EnumDefine.MenuTypeEnum Type { get; set; }
        public string Url { get; set; }
        public string Condition { get; set; }
        public EnumDefine.MenuPositionEnum Position { get; set; }
        public string LanguageId { get; set; }
        public string StoreId { get; set; }
        public long Status { get; set; }
        public int Priority { get; set; }
        public string[] BannerIds { get; set; }
    }
    
}