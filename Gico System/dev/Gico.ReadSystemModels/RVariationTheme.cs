using Gico.Config;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RVariationTheme : BaseReadModel
    {
        [ProtoMember(1)]
        public int VariationThemeId { get; set; }
        [ProtoMember(2)]
        public string VariationThemeName { get; set; }
        [ProtoMember(3)]
        public EnumDefine.VariationThemeStatus VariationThemeStatus { get; set; }
        
    }
}
