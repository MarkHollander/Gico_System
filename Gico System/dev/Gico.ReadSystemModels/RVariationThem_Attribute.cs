using Gico.Config;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RVariationTheme_Attribute : BaseReadModel
    {
        [ProtoMember(1)]
        public int AttributeId { get; set; }

        [ProtoMember(2)]
        public string AttributeName { get; set; }
        [ProtoMember(3)]
        public int VariationThemeId { get; set; }
        [ProtoMember(4)]
        public string VariationThemeName { get; set; }
    }
}
