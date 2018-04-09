using Gico.Config;
using Gico.ReadSystemModels.Banner;
using Gico.SystemCommands.Banner;
using Gico.SystemModels.Models.Banner;
using Gico.SystemModels.Request.Banner;
using Gico.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemAppService.Mapping.Banner
{
    public static class BannerItemMapping
    {
        public static BannerItemViewModel ToModel(this RBannerItem request)
        {
            if (request == null) return null;
            return new BannerItemViewModel()
            {
                Id = request.Id,
                BannerId = request.BannerId,
                BannerItemName = request.BannerItemName,
                TargetUrl = request.TargetUrl,
                ImageUrl = request.ImageUrl,
                BackgroundRGB = request.BackgroundRGB,
                Status = request.Status,
                IsDefault = request.IsDefault,
                StartDateUtc = request.StartDateUtc,
                EndDateUtc = request.EndDateUtc,
                Version = request.Version
            };
        }

        public static BannerItemAddCommand ToCommandAdd(this BannerItemAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new BannerItemAddCommand
            {
                BannerId = request.BannerId,
                BannerItemName = request.BannerItemName,
                TargetUrl = request.TargetUrl,
                ImageUrl = request.ImageUrl,
                BackgroundRGB = request.BackgroundRGB,
                Status = request.Status,
                IsDefault = request.IsDefault,
                StartDateUtc = request.StartDateUtc,
                EndDateUtc = request.EndDateUtc,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
                CreatedUid = userId
            };
        }
        public static BannerItemChangeCommand ToCommandChange(this BannerItemAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new BannerItemChangeCommand
            {
                Id = request.Id,
                BannerId = request.BannerId,
                BannerItemName = request.BannerItemName,
                TargetUrl = request.TargetUrl,
                ImageUrl = request.ImageUrl,
                BackgroundRGB = request.BackgroundRGB,
                Status = request.Status,
                IsDefault = request.IsDefault,
                StartDateUtc = request.StartDateUtc,
                EndDateUtc = request.EndDateUtc,
                UpdatedUid = userId
            };
        }

        public static BannerItemRemoveCommand ToCommandRemove(this BannerItemRemoveRequest request,string uid)
        {
            if (request == null) return null;
            return  new BannerItemRemoveCommand()
            {
                Id = request.Id,
                BannerId = request.BannerId,
                UpdatedUserId = uid
            };
        }
    }
}
