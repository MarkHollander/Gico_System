using Gico.Config;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.Banner
{
    [ProtoContract]
    public class RBanner : BaseReadModel
    {
        [ProtoMember(1)]
        public string BannerName { get; set; }
        [ProtoMember(2)]
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        [ProtoMember(3)]
        public string BackgroundRGB { get; set; }
    }
}
