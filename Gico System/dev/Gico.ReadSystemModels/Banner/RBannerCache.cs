using System;
using System.Collections.Generic;
using System.Linq;
using Gico.Config;
using Gico.SystemEvents.Cache.PageBuilder;
using ProtoBuf;

namespace Gico.ReadSystemModels.Banner
{
    [ProtoContract]
    public class RBannerCache : BaseReadModel
    {
        public RBannerCache()
        {

        }
        public RBannerCache(BannerCacheEvent mesage)
        {
            Id = mesage.Id;
            BannerName = mesage.BannerName;
            Status = mesage.Status;
            BackgroundRGB = mesage.BackgroundRgb;
            UpdatedDateUtc = mesage.UpdatedDateUtc;
            CreatedDateUtc = mesage.CreatedDateUtc;
            CreatedUid = mesage.CreatedUid;
            UpdatedUid = mesage.UpdatedUid;
            BannerItems = mesage.BannerItems?.Select(p => new RBannerItemCache(p)).ToArray();
        }

        [ProtoMember(1)]
        public string BannerName { get; set; }
        [ProtoMember(2)]
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        [ProtoMember(3)]
        public string BackgroundRGB { get; set; }
        [ProtoMember(4)]
        public RBannerItemCache[] BannerItems { get; set; }
    }

    public class RBannerItemCache : BaseReadModel
    {
        public RBannerItemCache()
        {

        }

        public RBannerItemCache(BannerItemCacheEvent bannerItemCache)
        {
            Id = bannerItemCache.Id;
            BannerItemName = bannerItemCache.BannerItemName;
            BannerId = bannerItemCache.BannerId;
            TargetUrl = bannerItemCache.TargetUrl;
            StartDateUtc = bannerItemCache.StartDateUtc;
            EndDateUtc = bannerItemCache.EndDateUtc;
            Status = bannerItemCache.Status;
            IsDefault = bannerItemCache.IsDefault;
            BackgroundRGB = bannerItemCache.BackgroundRgb;
            UpdatedDateUtc = bannerItemCache.UpdatedDateUtc;
            CreatedDateUtc = bannerItemCache.CreatedDateUtc;
            CreatedUid = bannerItemCache.CreatedUid;
            UpdatedUid = bannerItemCache.UpdatedUid;
        }
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