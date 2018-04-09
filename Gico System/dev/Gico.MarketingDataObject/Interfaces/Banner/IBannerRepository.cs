using Gico.Config;
using Gico.ReadSystemModels.Banner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.MarketingDataObject.Interfaces.Banner
{
    public interface IBannerRepository
    {
        Task<RBanner> GetById(string id);
        Task<RBanner[]> GetById(string[] ids);

        Task<RBanner[]> Search(string id, string bannerName, EnumDefine.CommonStatusEnum status, RefSqlPaging paging);

        Task<RBanner[]> GetByMenuId(string menuId);

        Task<bool> Add(Gico.SystemDomains.Banner.Banner banner);

        Task<bool> Change(Gico.SystemDomains.Banner.Banner banner);

        Task ChangeBannerStatus(string id, EnumDefine.CommonStatusEnum bannerStatus, string updatedUid, DateTime updatedDate);
    }
}
