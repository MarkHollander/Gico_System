using Gico.Config;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.PageBuilder
{
    [ProtoContract]
    public class RTemplateConfig : BaseReadModel
    {
        [ProtoMember(1)]
        public string TemplateId { get; private set; }
        [ProtoMember(2)]
        public string TemplatePositionCode { get; private set; }
        [ProtoMember(3)]
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; private set; }
        [ProtoMember(4)]
        public string ComponentId { get; private set; }
        [ProtoMember(5)]
        public string PathToView { get; private set; }
        [ProtoMember(6)]
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        [ProtoMember(7)]
        public string DataSource { get; private set; }
    }
}
