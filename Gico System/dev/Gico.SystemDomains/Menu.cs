using Gico.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.ReadSystemModels.Banner;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;

namespace Gico.SystemDomains
{
    public class Menu : BaseDomain
    {
        public Menu()
        {

        }

        public Menu(RMenu rMenu)
        {
            Id = rMenu.Id;
            ParentId = rMenu.ParentId;
            Name = rMenu.Name;
            Type = rMenu.Type;
            Url = rMenu.Url;
            Condition = rMenu.Condition;
            Position = rMenu.Position;
            CreatedDateUtc = rMenu.CreatedDateUtc;
            UpdatedDateUtc = rMenu.UpdatedDateUtc;
            CreatedUid = rMenu.CreatedUid;
            UpdatedUid = rMenu.UpdatedUid;
            LanguageId = rMenu.LanguageId;
            Status = rMenu.Status;
            StoreId = rMenu.StoreId;
            Priority = rMenu.Priority;
        }

        public Menu(RMenu rMenu, RBanner[] baners) : this(rMenu)
        {
            Banners = baners?.Select(p => new Banner.Banner(p)).ToList();
        }

        #region Publish method
        public void Add(MenuAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            LanguageId = command.LanguageId;
            Code = string.Empty;
            CreatedDateUtc = Extensions.GetCurrentDateUtc();
            CreatedUid = string.Empty;
            UpdatedDateUtc = Extensions.GetCurrentDateUtc();
            UpdatedUid = string.Empty;
            ParentId = command.ParentId;
            Name = command.Name;
            Type = (EnumDefine.MenuTypeEnum)command.Type;
            Url = command.Url;
            Condition = command.Condition;
            Position = command.Position;
            StoreId = command.StoreId;
            Status = command.Status;
            Priority = command.Priority;
            AddOrChangeEvent();
        }
        public void Change(MenuChangeCommand command)
        {
            Id = command.Id;
            LanguageId = command.LanguageId;
            Code = string.Empty;
            UpdatedDateUtc = Extensions.GetCurrentDateUtc();
            UpdatedUid = string.Empty;
            ParentId = command.ParentId;
            Name = command.Name;
            Type = (EnumDefine.MenuTypeEnum)command.Type;
            Url = command.Url;
            Condition = command.Condition;
            Position = (EnumDefine.MenuPositionEnum)command.Position;
            StoreId = command.StoreId;
            Status = command.Status;
            Priority = command.Priority;
            AddOrChangeEvent();
        }

        public Banner.Banner AddBanner(RBanner rbanner)
        {
            var banner = new Banner.Banner(rbanner);
            return AddBanner(banner);

        }
        public Banner.Banner AddBanner(Banner.Banner banner)
        {
            if (Banners == null)
            {
                Banners = new List<Banner.Banner>();
            }
            if (Banners.Any(p => p.Id == banner.Id))
            {
                throw new Exception("Banner is exist.");
            }
            Banners.Add(banner);

            AddOrChangeEvent();
            return banner;
        }

        public Banner.Banner RemoveBanner(string bannerId)
        {
            if (Banners == null)
            {
                Banners = new List<Banner.Banner>();
            }
            var banner = Banners.FirstOrDefault(p => p.Id == bannerId);
            if (banner == null)
            {
                throw new Exception("Banner is not null.");
            }
            Banners.Remove(banner);

            AddOrChangeEvent();
            return banner;
        }

        #endregion

        #region Event
        private void AddOrChangeEvent()
        {
            MenuCacheAddOrChangeEvent @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }
        #endregion

        #region Properties
        public string ParentId { get; private set; }
        public string Name { get; private set; }
        public EnumDefine.MenuTypeEnum Type { get; private set; }
        public string Url { get; private set; }
        public string Condition { get; private set; }
        public EnumDefine.MenuPositionEnum Position { get; private set; }
        public int Priority { get; private set; }
        public IList<Banner.Banner> Banners { get; private set; }
        #endregion



        #region Convert

        public MenuCacheAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new MenuCacheAddOrChangeEvent()
            {
                Name = this.Name,
                Id = this.Id,
                Type = this.Type,
                LanguageId = this.LanguageId,
                Status = this.Status,
                Condition = this.Condition,
                StoreId = this.StoreId,
                Position = this.Position,
                ParentId = this.ParentId,
                Url = this.Url,
                Priority = this.Priority,
                BannerIds = this.Banners.Select(p=>p.Id).ToArray()
            };
        }
        #endregion

    }
}
