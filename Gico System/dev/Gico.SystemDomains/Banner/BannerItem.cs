using Gico.Config;
using Gico.Domains;
using Gico.SystemCommands.Banner;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.ReadSystemModels.Banner;
using Gico.SystemEvents.Cache.PageBuilder;

namespace Gico.SystemDomains.Banner
{
    public class BannerItem : BaseDomain
    {
        public BannerItem()
        {
        }

        public BannerItem(RBannerItem bannerItem)
        {
            Id = bannerItem.Id;
            BannerItemName = bannerItem.BannerItemName;
            BannerId = bannerItem.BannerId;
            TargetUrl = bannerItem.TargetUrl;
            ImageUrl = bannerItem.ImageUrl;
            StartDateUtc = bannerItem.StartDateUtc;
            EndDateUtc = bannerItem.EndDateUtc;
            Status = bannerItem.Status;
            IsDefault = bannerItem.IsDefault;
            BackgroundRGB = bannerItem.BackgroundRGB;
            UpdatedDateUtc = bannerItem.UpdatedDateUtc;
            CreatedDateUtc = bannerItem.CreatedDateUtc;
            CreatedUid = bannerItem.CreatedUid;
            UpdatedUid = bannerItem.UpdatedUid;
        }

        #region Publish method        
        public void Init(BannerItemAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            BannerItemName = command.BannerItemName ?? string.Empty;
            BannerId = command.BannerId ?? string.Empty;
            TargetUrl = command.TargetUrl ?? string.Empty;
            ImageUrl = command.ImageUrl ?? string.Empty;
            Status = command.Status;
            StartDateUtc = command.StartDateUtc;
            EndDateUtc = command.EndDateUtc;
            IsDefault = command.IsDefault;
            BackgroundRGB = command.BackgroundRGB;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid ?? string.Empty;
            UpdatedUid = command.CreatedUid ?? string.Empty;
        }
        public void Change(BannerItemChangeCommand command)
        {
            Id = command.Id;
            BannerItemName = command.BannerItemName;
            TargetUrl = command.TargetUrl;
            ImageUrl = command.ImageUrl;
            StartDateUtc = command.StartDateUtc;
            EndDateUtc = command.EndDateUtc;
            Status = command.Status;
            IsDefault = command.IsDefault;
            BackgroundRGB = command.BackgroundRGB;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.UpdatedUid;
        }

        public void Remove(BannerItemRemoveCommand command)
        {
            Status = EnumDefine.CommonStatusEnum.Deleted;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.UpdatedUserId;
        }
        #endregion

        #region Properties
        public string BannerItemName { get; set; }
        public string BannerId { get; set; }
        public string TargetUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public bool IsDefault { get; set; }
        public string BackgroundRGB { get; set; }
        #endregion
        #region Convert

        public BannerItemCacheEvent ToAddOrChangCacheEvent()
        {
            return new BannerItemCacheEvent()
            {
                Status = this.Status,
                Id = this.Id,
                UpdatedDateUtc = this.UpdatedDateUtc,
                UpdatedUid = this.UpdatedUid,
                CreatedUid = this.CreatedUid,
                CreatedDateUtc = this.CreatedDateUtc,
                LanguageId = this.LanguageId,
                Code = this.Code,
                ShardId = this.ShardId,
                BackgroundRgb = this.BackgroundRGB,
                StoreId = this.StoreId,
                StartDateUtc = this.StartDateUtc,
                BannerId = this.BannerId,
                BannerItemName = this.BannerItemName,
                TargetUrl = this.TargetUrl,
                IsDefault = this.IsDefault,
                ImageUrl = this.ImageUrl,
                EndDateUtc = this.EndDateUtc,
                
            };
        }

        #endregion
    }
}
