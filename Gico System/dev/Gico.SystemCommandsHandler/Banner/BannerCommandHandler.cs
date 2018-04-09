using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemCommands.Banner;
using Gico.SystemDomains.Banner;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.Banner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels.Banner;

namespace Gico.SystemCommandsHandler.Banner
{
    public class BannerCommandHandler : ICommandHandler<BannerAddCommand, ICommandResult>,
        ICommandHandler<BannerChangeCommand, ICommandResult>,
        ICommandHandler<BannerItemAddCommand, ICommandResult>,
        ICommandHandler<BannerItemChangeCommand, ICommandResult>,
        ICommandHandler<BannerRemoveCommand, ICommandResult>,
        ICommandHandler<BannerItemRemoveCommand, ICommandResult>
    {
        private readonly IBannerService _bannerService;
        private readonly ICommonService _commonService;
        private readonly IEventSender _eventSender;
        public BannerCommandHandler(IBannerService bannerService, ICommonService commonService, IEventSender eventSender)
        {
            _bannerService = bannerService;
            _commonService = commonService;
            _eventSender = eventSender;
        }

        public async Task<ICommandResult> Handle(BannerAddCommand mesage)
        {
            try
            {
                SystemDomains.Banner.Banner banner = new SystemDomains.Banner.Banner();
                banner.Add(mesage);
                await _bannerService.AddBanner(banner);

                await _eventSender.Notify(banner.Events);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = banner.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(BannerChangeCommand mesage)
        {
            try
            {
                ICommandResult result;
                var rBanner = await _bannerService.GetBannerById(mesage.Id);
                if (rBanner == null)
                {

                    result = new CommandResult()
                    {
                        Message = "Banner not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Banner_NotFound
                    };
                    return result;
                }
                RBannerItem[] rBannerItems = await _bannerService.GetBannerItemByBannerId(rBanner.Id);
                SystemDomains.Banner.Banner banner = new SystemDomains.Banner.Banner(rBanner, rBannerItems);
                banner.Change(mesage);
                await _bannerService.ChangeBanner(banner);

                await _eventSender.Notify(banner.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = banner.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(BannerItemAddCommand mesage)
        {
            try
            {
                ICommandResult result;
                var rBanner = await _bannerService.GetBannerById(mesage.BannerId);
                if (rBanner == null)
                {

                    result = new CommandResult()
                    {
                        Message = "Banner not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Banner_NotFound
                    };
                    return result;
                }
                RBannerItem[] rBannerItems = await _bannerService.GetBannerItemByBannerId(rBanner.Id);
                SystemDomains.Banner.Banner banner = new SystemDomains.Banner.Banner(rBanner, rBannerItems);
                BannerItem bannerItem = banner.AddItem(mesage);
                await _bannerService.AddBannerItem(bannerItem);

                await _eventSender.Notify(banner.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = bannerItem.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(BannerItemChangeCommand mesage)
        {
            try
            {
                ICommandResult result;
                var rBanner = await _bannerService.GetBannerById(mesage.BannerId);
                if (rBanner == null)
                {

                    result = new CommandResult()
                    {
                        Message = "Banner not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Banner_NotFound
                    };
                    return result;
                }
                RBannerItem[] rBannerItems = await _bannerService.GetBannerItemByBannerId(rBanner.Id);
                SystemDomains.Banner.Banner banner = new SystemDomains.Banner.Banner(rBanner, rBannerItems);

                BannerItem bannerItem = banner.ChangeItem(mesage);
                await _bannerService.ChangeBannerItem(bannerItem);

                await _eventSender.Notify(banner.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = bannerItem.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(BannerRemoveCommand mesage)
        {
            try
            {
                ICommandResult result;
                var rBanner = await _bannerService.GetBannerById(mesage.Id);
                if (rBanner == null)
                {

                    result = new CommandResult()
                    {
                        Message = "Banner not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Banner_NotFound
                    };
                    return result;
                }
                SystemDomains.Banner.Banner banner = new SystemDomains.Banner.Banner(rBanner);
                banner.Remove(mesage);
                await _bannerService.ChangeBannerStatus(banner.Id, banner.Status, banner.UpdatedUid, banner.UpdatedDateUtc);

                await _eventSender.Notify(banner.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = banner.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(BannerItemRemoveCommand mesage)
        {
            try
            {
                ICommandResult result;
                var rBanner = await _bannerService.GetBannerById(mesage.BannerId);
                if (rBanner == null)
                {

                    result = new CommandResult()
                    {
                        Message = "Banner not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Banner_NotFound
                    };
                    return result;
                }
                RBannerItem[] rBannerItems = await _bannerService.GetBannerItemByBannerId(rBanner.Id);
                SystemDomains.Banner.Banner banner = new SystemDomains.Banner.Banner(rBanner, rBannerItems);
                var bannerItem = banner.RemoveItem(mesage);
                await _bannerService.ChangeBannerItemStatus(bannerItem.Id, bannerItem.Status, bannerItem.UpdatedUid, bannerItem.UpdatedDateUtc);

                await _eventSender.Notify(banner.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = banner.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }
    }
}
