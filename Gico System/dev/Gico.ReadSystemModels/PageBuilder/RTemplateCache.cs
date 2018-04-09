using System.Collections.Generic;
using System.Linq;
using Gico.Config;
using Gico.SystemEvents.Cache.PageBuilder;
using ProtoBuf;

namespace Gico.ReadSystemModels.PageBuilder
{
    [ProtoContract]
    public class RTemplateCache : BaseReadModel
    {
        public RTemplateCache()
        {

        }

        public RTemplateCache(TemplateCacheEvent templateCacheEvent)
        {
            Id = templateCacheEvent.Id;
            TemplateName = templateCacheEvent.TemplateName;
            Thumbnail = templateCacheEvent.Thumbnail;
            Structure = templateCacheEvent.Structure;
            PathToView = templateCacheEvent.PathToView;
            Status = templateCacheEvent.Status;
            Code = templateCacheEvent.Code;
            PageType = templateCacheEvent.PageType;
            PageParameters = templateCacheEvent.PageParameters;
            UpdatedDateUtc = templateCacheEvent.UpdatedDateUtc;
            CreatedDateUtc = templateCacheEvent.CreatedDateUtc;
            CreatedUid = templateCacheEvent.CreatedUid;
            UpdatedUid = templateCacheEvent.CreatedUid;
            TemplateConfigs = templateCacheEvent.TemplateConfigs?.Select(p => new RTemplateConfigCache(p)).ToArray();
        }
        #region Properties
        [ProtoMember(1)]
        public string TemplateName { get; set; }
        [ProtoMember(2)]
        public string Thumbnail { get; set; }
        [ProtoMember(3)]
        public string Structure { get; set; }
        [ProtoMember(4)]
        public string PathToView { get; set; }
        [ProtoMember(5)]
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        [ProtoMember(6)]
        public EnumDefine.TemplatePageTypeEnum PageType { get; set; }
        [ProtoMember(7)]
        public string PageParameters { get; set; }
        [ProtoMember(8)]
        public RTemplateConfigCache[] TemplateConfigs { get; set; }
        #endregion
    }
    [ProtoContract]
    public class RTemplateConfigCache : BaseReadModel
    {
        public RTemplateConfigCache()
        {

        }

        public RTemplateConfigCache(TemplateConfigCacheEvent @event)
        {
            Id = @event.Id;
            TemplatePositionCode = @event.TemplatePositionCode;
            ComponentType = @event.ComponentType;
            ComponentId = @event.ComponentId;
            PathToView = @event.PathToView;
            Status = @event.Status;
            DataSource = @event.DataSource;
            UpdatedDateUtc = @event.UpdatedDateUtc;
            CreatedDateUtc = @event.CreatedDateUtc;
            CreatedUid = @event.CreatedUid;
            UpdatedUid = @event.UpdatedUid;
        }
        [ProtoMember(1)]
        public string TemplatePositionCode { get; set; }
        [ProtoMember(2)]
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }
        [ProtoMember(3)]
        public string ComponentId { get; set; }
        [ProtoMember(4)]
        public string PathToView { get; set; }
        [ProtoMember(5)]
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        [ProtoMember(6)]
        public string DataSource { get; set; }
    }
}