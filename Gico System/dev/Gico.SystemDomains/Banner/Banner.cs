using Gico.Config;
using Gico.Domains;
using Gico.SystemCommands.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gico.ReadSystemModels.Banner;
using Gico.SystemEvents.Cache.PageBuilder;

namespace Gico.SystemDomains.Banner
{
    public class Banner : BaseDomain
    {
        public Banner()
        {
        }

        public Banner(RBanner banner)
        {
            Id = banner.Id;
            BannerName = banner.BannerName;
            Status = banner.Status;
            BackgroundRGB = banner.BackgroundRGB;
            UpdatedDateUtc = banner.UpdatedDateUtc;
            CreatedDateUtc = banner.CreatedDateUtc;
            CreatedUid = banner.CreatedUid;
            UpdatedDateUtc = banner.UpdatedDateUtc;
        }
        public Banner(RBanner banner, RBannerItem[] bannerItems) : this(banner)
        {
            BannerItems = bannerItems?.Select(p => new BannerItem(p)).ToList();
        }

        #region Publish method        
        public void Add(BannerAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            BannerName = command.BannerName ?? string.Empty;
            Status = command.Status;
            BackgroundRGB = command.BackgroundRGB ?? string.Empty;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid ?? string.Empty;
            UpdatedUid = command.CreatedUid ?? string.Empty;

            AddEvent(this.ToAddOrChangCacheEvent());
        }
        public void Change(BannerChangeCommand command)
        {
            Id = command.Id;
            BannerName = command.BannerName;
            Status = command.Status;
            BackgroundRGB = command.BackgroundRGB;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.UpdatedUid;

            AddEvent(this.ToAddOrChangCacheEvent());
        }

        public void Remove(BannerRemoveCommand command)
        {
            Status = EnumDefine.CommonStatusEnum.Deleted;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.UpdatedUid;

            AddEvent(this.ToRemoveEvent());
        }

        public BannerItem AddItem(BannerItemAddCommand command)
        {
            if (BannerItems == null)
            {
                BannerItems = new List<BannerItem>();
            }
            BannerItem bannerItem = new BannerItem();
            bannerItem.Init(command);
            BannerItems.Add(bannerItem);

            AddEvent(this.ToAddOrChangCacheEvent());
            return bannerItem;
        }
        public BannerItem ChangeItem(BannerItemChangeCommand command)
        {
            if (BannerItems == null)
            {
                BannerItems = new List<BannerItem>();
            }
            BannerItem bannerItem = BannerItems.FirstOrDefault(p => p.Id == command.Id);
            if (bannerItem == null)
            {
                throw new Exception("BannerItem not found.");
            }
            bannerItem.Change(command);

            AddEvent(this.ToAddOrChangCacheEvent());
            return bannerItem;
        }

        public BannerItem RemoveItem(BannerItemRemoveCommand command)
        {
            if (BannerItems == null)
            {
                BannerItems = new List<BannerItem>();
            }
            BannerItem bannerItem = BannerItems.FirstOrDefault(p => p.Id == command.Id);
            if (bannerItem == null)
            {
                throw new Exception("BannerItem not found.");
            }
            bannerItem.Remove(command);

            AddEvent(this.ToAddOrChangCacheEvent());
            return bannerItem;

        }
        #endregion

        #region Properties

        public string BannerName { get; private set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        public string BackgroundRGB { get; private set; }
        public List<BannerItem> BannerItems { get; private set; }

        #endregion

        #region Convert

        public BannerCacheEvent ToAddOrChangCacheEvent()
        {
            return new BannerCacheEvent()
            {
                Status = this.Status,
                Id = this.Id,
                UpdatedDateUtc = this.UpdatedDateUtc,
                UpdatedUid = this.UpdatedUid,
                CreatedUid = this.CreatedUid,
                CreatedDateUtc = this.CreatedDateUtc,
                LanguageId = this.LanguageId,
                BannerName = this.BannerName,
                Code = this.Code,
                ShardId = this.ShardId,
                BackgroundRgb = this.BackgroundRGB,
                StoreId = this.StoreId,
                BannerItems = this.BannerItems?.Select(p => p.ToAddOrChangCacheEvent()).ToArray()
            };
        }

        public BannerRemoveEvent ToRemoveEvent()
        {
            return new BannerRemoveEvent()
            {
                Id = this.Id
            };
        }
        #endregion
    }
}
