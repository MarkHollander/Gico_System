using Gico.Caching.Redis;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.MarketingDataObject.Interfaces.Banner;
using Gico.ReadSystemModels.Banner;
using Gico.SystemCommands.Banner;
using Gico.SystemDomains.Banner;
using Gico.SystemService.Interfaces.Banner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Gico.MarketingCacheStorage.Interfaces;
using Gico.ReadSystemModels.PageBuilder;

namespace Gico.SystemService.Implements.Banner
{
    public class BannerService : IBannerService
    {
        private readonly ICommandSender _commandService;
        private readonly IBannerRepository _bannerRepository;
        private readonly IBannerItemRepository _bannerItemRepository;
        private readonly IComponentCacheStorage _componentCacheStorage;
        public BannerService(ICommandSender commandService, IBannerRepository bannerRepository, IBannerItemRepository bannerItemRepository, IComponentCacheStorage componentCacheStorage)
        {
            this._commandService = commandService;
            this._bannerRepository = bannerRepository;
            this._bannerItemRepository = bannerItemRepository;
            _componentCacheStorage = componentCacheStorage;
        }

        #region Dd

        public async Task<RBanner[]> SearchBanner(string id, string bannerName, EnumDefine.CommonStatusEnum bannerStatus, RefSqlPaging paging)
        {
            return await _bannerRepository.Search(id, bannerName, bannerStatus, paging);
        }
        public async Task<RBanner> GetBannerById(string id)
        {
            return await _bannerRepository.GetById(id);
        }
        public async Task<RBanner[]> GetBannerByIds(string[] ids)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddBanner(Gico.SystemDomains.Banner.Banner banner)
        {
            return await _bannerRepository.Add(banner);
        }
        public async Task<bool> ChangeBanner(Gico.SystemDomains.Banner.Banner banner)
        {
            return await _bannerRepository.Change(banner);
        }
        public async Task ChangeBannerStatus(string id, EnumDefine.CommonStatusEnum status, string updatedUid, DateTime updatedDate)
        {
            await _bannerRepository.ChangeBannerStatus(id, status, updatedUid, updatedDate);
        }
        public async Task<RBannerItem[]> SearchBannerItem(string id, string bannerItemName, string bannerId, EnumDefine.CommonStatusEnum status, bool isDefault, DateTimeRange startDate, DateTimeRange endDate, RefSqlPaging paging)
        {
            return await _bannerItemRepository.Search(id, bannerItemName, bannerId, status, isDefault, startDate, endDate, paging);
        }
        public async Task<RBannerItem> GetBannerItemById(string id)
        {
            return await _bannerItemRepository.GetById(id);
        }
        public async Task<RBannerItem[]> GetBannerItemByBannerId(string id)
        {
            return await _bannerItemRepository.GetByBannerId(id);
        }
        public async Task<RBannerItem[]> GetBannerItemByBannerId(string[] ids)
        {
            return await _bannerItemRepository.GetById(ids);
        }
        public async Task<bool> AddBannerItem(BannerItem bannerItem)
        {
            return await _bannerItemRepository.Add(bannerItem);
        }
        public async Task<bool> ChangeBannerItem(BannerItem bannerItem)
        {
            return await _bannerItemRepository.Change(bannerItem);
        }
        public async Task ChangeBannerItemStatus(string id, EnumDefine.CommonStatusEnum status, string userId, DateTime updatedDate)
        {
            await _bannerItemRepository.ChangeStatus(id, status, userId, updatedDate);
        }
        public async Task<RBanner[]> GetBannerByMenuId(string menuId)
        {
            return await _bannerRepository.GetByMenuId(menuId);
        }
        #endregion

        #region Sencommand

        public async Task<CommandResult> SendCommand(BannerItemAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(BannerItemChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(BannerItemRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(BannerAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(BannerChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(BannerRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

        #region Cache

        public async Task AddToCache(RBannerCache rBannerCache)
        {
            RComponentCache cache = new RComponentCache()
            {
                Id = rBannerCache.Id,
                ComponentType = EnumDefine.TemplateConfigComponentTypeEnum.Banner,
                ComponentSettingObject = rBannerCache
            };
            await _componentCacheStorage.AddToCache(cache);
        }
        public async Task RemoveToCache(string id)
        {
            await _componentCacheStorage.RemoveToCache(id);
        }
        public async Task<RBannerCache> GetFromCache(string id)
        {
            var component = await _componentCacheStorage.GetFromCache(id);
            return component?.ComponentSettingObject as RBannerCache;
        }

        #endregion
    }
}
