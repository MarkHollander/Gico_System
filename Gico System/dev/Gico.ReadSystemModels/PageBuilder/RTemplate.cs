using Gico.Config;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.PageBuilder
{
    [ProtoContract]
    public class RTemplate : BaseReadModel
    {
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
        #endregion
    }
}
