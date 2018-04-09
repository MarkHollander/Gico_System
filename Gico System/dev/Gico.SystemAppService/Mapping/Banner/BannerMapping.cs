using Gico.ReadSystemModels.Banner;
using Gico.SystemCommands.Banner;
using Gico.SystemModels.Models.Banner;
using Gico.SystemModels.Request.Banner;
using Gico.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.Models.Response;

namespace Gico.SystemAppService.Mapping.Banner
{
    public static class BannerMapping
    {
        public static BannerViewModel ToModel(this RBanner request)
        {
            if (request == null) return null;
            return new BannerViewModel()
            {
                Id = request.Id,
                BannerName = request.BannerName,
                BackgroundRgb = request.BackgroundRGB,
                Status = request.Status,
                Version = request.Version,
            };
        }
        public static KeyValueTypeStringModel ToAutocompleteModel(this RBanner item)
        {
            if (item == null) return null;
            return new KeyValueTypeStringModel()
            {
                Value = item.Id,
                Text = item.BannerName,
            };
        }
        public static BannerAddCommand ToCommandAdd(this BannerAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new BannerAddCommand
            {
                BannerName = request.BannerName,
                BackgroundRGB = request.BackgroundRGB,
                Status = request.Status,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
                CreatedUid = userId,
            };
        }
        public static BannerChangeCommand ToCommandChange(this BannerAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new BannerChangeCommand
            {
                Id = request.Id,
                BannerName = request.BannerName,
                BackgroundRGB = request.BackgroundRGB,
                Status = request.Status,
                UpdatedUid = userId
            };
        }
        public static  BannerRemoveCommand ToCommandRemove(this BannerRemoveRequest command, string userId)
        {
            if (command == null) return null;
            return new BannerRemoveCommand()
            {
                Id = command.Id,
                UpdatedUid = userId
            };
        }
    }
}
