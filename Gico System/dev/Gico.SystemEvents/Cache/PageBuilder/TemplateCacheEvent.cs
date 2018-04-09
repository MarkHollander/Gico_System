using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.Events;

namespace Gico.SystemEvents.Cache.PageBuilder
{
    public class TemplateCacheEvent : BaseEvent
    {
        #region Properties
        public string TemplateName { get; set; }
        public string Thumbnail { get; set; }
        public string Structure { get; set; }
        public string PathToView { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public EnumDefine.TemplatePageTypeEnum PageType { get; set; }
        public string PageParameters { get; set; }
        public TemplateConfigCacheEvent[] TemplateConfigs { get;   set; }
        #endregion
    }
}
