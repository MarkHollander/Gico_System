using Gico.Config;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.Banner
{
    [ProtoContract]
    public class RBannerItem : BaseReadModel
    {
        [ProtoMember(1)]
        public string BannerItemName { get; set; }
        [ProtoMember(2)]
        public string BannerId { get; set; }
        [ProtoMember(3)]
        public string TargetUrl { get; set; }
        [ProtoMember(4)]
        public string ImageUrl { get; set; }
        [ProtoMember(5)]
        public DateTime? StartDateUtc { get; set; }
        [ProtoMember(6)]
        public DateTime? EndDateUtc { get; set; }
        [ProtoMember(7)]
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        [ProtoMember(8)]
        public bool IsDefault { get; set; }
        [ProtoMember(9)]
        public string BackgroundRGB { get; set; }
    }
}
