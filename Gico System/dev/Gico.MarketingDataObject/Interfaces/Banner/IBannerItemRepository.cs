using Gico.Config;
using Gico.ReadSystemModels.Banner;
using Gico.SystemDomains.Banner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.MarketingDataObject.Interfaces.Banner
{
    public interface IBannerItemRepository
    {
        Task<RBannerItem> GetById(string id);
        Task<RBannerItem[]> GetById(string[] ids);
        Task<RBannerItem[]> GetByBannerId(string id);

        Task<RBannerItem[]> Search(string id, string bannerItemName, string bannerId, EnumDefine.CommonStatusEnum status, bool isDefault, DateTimeRange startDate, DateTimeRange endDate, RefSqlPaging paging);

        Task<bool> Add(BannerItem bannerItem);

        Task<bool> Change(BannerItem bannerItem);
        Task ChangeStatus(string id, EnumDefine.CommonStatusEnum status, string userId, DateTime updatedDate);
    }
}
