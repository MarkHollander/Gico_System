using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RCategory_VariationTheme_Mapping : BaseReadModel
    {
        [ProtoMember(1)]
        public int VariationThemeId { get; set; }
        [ProtoMember(2)]
        public string CategoryId { get; set; }
        [ProtoMember(3)]
        public string VariationThemeName { get; set; }
    }
}
