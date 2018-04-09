using Gico.Config;
using Gico.Events;

namespace Gico.SystemEvents.Cache.PageBuilder
{
    public class TemplateConfigCacheEvent : BaseEvent
    {
        #region Properties
        public string TemplateId { get; set; }
        public string TemplatePositionCode { get; set; }
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }
        public string ComponentId { get; set; }
        public string PathToView { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public string DataSource { get; set; }

        #endregion
    }
}