using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels.Banner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.SystemDomains.Banner;
using Gico.SystemCommands.Banner;

namespace Gico.SystemService.Interfaces.Banner
{
    public interface IBannerService
    {
        #region Db
        Task<RBanner[]> SearchBanner(string id, string bannerName, EnumDefine.CommonStatusEnum bannerStatus, RefSqlPaging paging);
        Task<RBanner> GetBannerById(string id);
        Task<RBanner[]> GetBannerByIds(string[] ids);
        Task<bool> AddBanner(Gico.SystemDomains.Banner.Banner banner);
        Task<bool> ChangeBanner(Gico.SystemDomains.Banner.Banner banner);
        Task ChangeBannerStatus(string id, EnumDefine.CommonStatusEnum bannerStatus, string updatedUid, DateTime updatedDate);
        Task<RBannerItem[]> SearchBannerItem(string id, string bannerItemName, string bannerId, EnumDefine.CommonStatusEnum status, bool isDefault, DateTimeRange startDate, DateTimeRange endDate, RefSqlPaging paging);
        Task<RBannerItem> GetBannerItemById(string id);
        Task<RBannerItem[]> GetBannerItemByBannerId(string id);
        Task<RBannerItem[]> GetBannerItemByBannerId(string[] ids);
        Task<bool> AddBannerItem(BannerItem bannerItem);
        Task<bool> ChangeBannerItem(BannerItem bannerItem);
        Task ChangeBannerItemStatus(string id, EnumDefine.CommonStatusEnum status, string userId, DateTime updatedDate);
        Task<RBanner[]> GetBannerByMenuId(string menuId);
        #endregion

        #region Sencommand

        Task<CommandResult> SendCommand(BannerItemAddCommand command);
        Task<CommandResult> SendCommand(BannerItemChangeCommand command);
        Task<CommandResult> SendCommand(BannerItemRemoveCommand command);
        Task<CommandResult> SendCommand(BannerAddCommand command);
        Task<CommandResult> SendCommand(BannerChangeCommand command);
        Task<CommandResult> SendCommand(BannerRemoveCommand command);

        #endregion

        #region Cache

        Task AddToCache(RBannerCache rBannerCache);
        Task RemoveToCache(string id);
        Task<RBannerCache> GetFromCache(string id);

        #endregion


    }
}
