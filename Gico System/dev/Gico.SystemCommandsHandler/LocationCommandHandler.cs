using Gico.AddressDomain;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadAddressModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;

namespace Gico.SystemCommandsHandler
{
    public class LocationCommandHandler : ICommandHandler<ProvinceUpdateCommand, ICommandResult>, 
                                        ICommandHandler<LocationRemoveCommand, ICommandResult>,
                                        ICommandHandler<DistrictUpdateCommand, ICommandResult>,
                                        ICommandHandler<WardUpdateCommand, ICommandResult>
    {
        private readonly ILocationService _locationService;
        private readonly ICommonService _commonService;

        public LocationCommandHandler(ILocationService locationService, ICommonService commonService)
        {
            _locationService = locationService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(ProvinceUpdateCommand mesage)
        {
            try
            {
                Province province = new Province();
                province.Init(mesage);
                await _locationService.ChangeToDb(province);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = province.Id,
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

        public async Task<ICommandResult> Handle(DistrictUpdateCommand mesage)
        {
            try
            {
                District district = new District();
                district.Init(mesage);
                await _locationService.UpdateDistrict(district);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = district.Id,
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

        public async Task<ICommandResult> Handle(WardUpdateCommand mesage)
        {
            try
            {
                Ward ward = new Ward();
                ward.Init(mesage);
                await _locationService.UpdateWard(ward);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = ward.Id,
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



        public async Task<ICommandResult> Handle(LocationRemoveCommand mesage)
        {
            try
            {
                ICommandResult result;
                Province province = new Province();
                province.Remove(mesage);

                #region CheckExits

                if (mesage.TypeLocation == 1)
                {
                    var location = await _locationService.GetProvinceById(mesage.ProvinceId);
                    if (location == null)
                    {
                        result = new CommandResult()
                        {
                            Message = "Province not found",
                            ObjectId = "",
                            Status = CommandResult.StatusEnum.Fail,
                            ResourceName = ResourceKey.Location_NotFound
                        };
                        return result;
                    }
                    await _locationService.ChangeProvinceStatus(mesage.ProvinceId, province.Status, province.UpdatedUid, province.UpdatedDateUtc);
                }
                if (mesage.TypeLocation == 2)
                {
                    var location = await _locationService.GetDistrictById(mesage.DistricId);
                    if (location == null)
                    {
                        result = new CommandResult()
                        {
                            Message = "District not found",
                            ObjectId = "",
                            Status = CommandResult.StatusEnum.Fail,
                            ResourceName = ResourceKey.Location_NotFound
                        };
                        return result;
                    }
                    await _locationService.ChangeDistrictStatus(mesage.DistricId, province.Status, province.UpdatedUid, province.UpdatedDateUtc);
                }
                if (mesage.TypeLocation == 3)
                {
                    var location = await _locationService.GetWardById(mesage.WardId);
                    if (location == null)
                    {
                        result = new CommandResult()
                        {
                            Message = "Ward not found",
                            ObjectId = "",
                            Status = CommandResult.StatusEnum.Fail,
                            ResourceName = ResourceKey.Location_NotFound
                        };
                        return result;
                    }
                    await _locationService.ChangeWardStatus(mesage.WardId, province.Status, province.UpdatedUid, province.UpdatedDateUtc);
                }
                if (mesage.TypeLocation == 4)
                {
                    var location = await _locationService.GetStreetById(mesage.StreetId);
                    if (location == null)
                    {
                        result = new CommandResult()
                        {
                            Message = "Ward not found",
                            ObjectId = "",
                            Status = CommandResult.StatusEnum.Fail,
                            ResourceName = ResourceKey.Location_NotFound
                        };
                        return result;
                    }
                    await _locationService.ChangeStreetStatus(mesage.StreetId, province.Status, province.UpdatedUid, province.UpdatedDateUtc);
                }

                #endregion
                
                //await _eventSender.Notify(banner.Events);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = province.Id,
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
