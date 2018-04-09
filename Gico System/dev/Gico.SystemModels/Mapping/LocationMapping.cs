using Gico.ReadAddressModels;
using Gico.SystemCommands;
using Gico.SystemModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.Common;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemModels.Mapping
{
    public static class LocationMapping
    {
        public static LocationViewModel ToModel(this RProvince province)
        {
            if (province == null)
            {
                return null;
            }

            return new LocationViewModel()
            {
                ProvinceName = province.ProvinceName,
                Id = province.Id
            };
        }

        public static LocationDetailResponse ToModelDetail(this RProvince province)
        {
            if (province == null)
            {
                return null;
            }

            return new LocationDetailResponse()
            {
                Id = province.Id,
                ProvinceName = province.ProvinceName,
                ProvinceNameEN = province.ProvinceNameEN,
            };
        }

        public static LocationDetailResponse ToModelDistrictDetail(this RDistrict district)
        {
            if (district == null)
            {
                return null;
            }

            return new LocationDetailResponse()
            {
                Id = district.Id,
                DistrictName = district.DistrictName,
                DistrictNameEN = district.DistrictNameEN,
                Prefix = district.Prefix,
                ShortName = district.ShortName,
                ProvinceId = district.ProvinceId,
                ProvinceName = district.ProvinceName
            };
        }

        public static LocationDetailResponse ToModelWardDetail(this RWard ward)
        {
            if (ward == null)
            {
                return null;
            }

            return new LocationDetailResponse()
            {
                Id = ward.Id,
                WardName = ward.WardName,
                WardNameEN = ward.WardNameEN,
                Prefix = ward.Prefix,
                ShortName = ward.ShortName,
                ProvinceId = ward.ProvinceId,
                ProvinceName = ward.ProvinceName,
                DistrictId = ward.DistrictId,
                DistrictName = ward.DistrictName
            };
        }

        public static LocationDetailResponse ToModelStreetDetail(this RStreet street)
        {
            if (street == null)
            {
                return null;
            }

            return new LocationDetailResponse()
            {
                Id = street.Id,
                StreetName = street.StreetName,
                StreetNameEN = street.StreetNameEN,
            };
        }

        public static DistricsViewModel ToModelDistric(this RDistrict district)
        {
            if (district == null)
            {
                return null;
            }

            return new DistricsViewModel()
            {
                DistricName = district.DistrictName,
                Id = district.Id
            };
        }

        public static WardViewModel ToModelWard(this RWard ward)
        {
            if (ward == null)
            {
                return null;
            }

            return new WardViewModel()
            {
                WardName = ward.WardName,
                Id = ward.Id
            };
        }

        public static StreetViewModel ToModelStreet(this RStreet street)
        {
            if (street == null)
            {
                return null;
            }

            return new StreetViewModel()
            {
                StreetName = street. StreetName,
                Id = street.Id
            };
        }

        public static ProvinceUpdateCommand ToUpdateCommand(this LocationUpdateRequest province, string userId)
        {
            if (province == null)
                return null;
            return new ProvinceUpdateCommand()
            {
                Id = province.Id,
                Prefix = province.Prefix,
                ProvinceName = province.ProvinceName,
                ProvinceNameEN = province.ProvinceNameEN,
                Status = province.Status,
                ShortName = province.ShortName,
                Priority = province.Priority,
                Latitude = province.Latitude,
                Longitude = province.Longitude,
                RegionId = province.RegionId,
                UpdatedDateUtc = Extensions.GetCurrentDateUtc(),
                CreatedDateUtc = province.CreatedDateUtc,
                CreatedUid = province.CreatedUid,
                UpdatedUid = userId
            };
        }

        public static DistrictUpdateCommand ToUpdateDistrictCommand(this LocationUpdateRequest district, string userId)
        {
            if (district == null)
                return null;
            return new DistrictUpdateCommand()
            {
                Id = district.Id,
                ProvinceId = district.ProvinceId,
                Prefix = district.Prefix,
                DistrictName = district.DistrictName,
                DistrictNameEN = district.DistrictNameEN,
                Status = district.Status,
                ShortName = district.ShortName,
                Priority = district.Priority,
                Latitude = district.Latitude,
                Longitude = district.Longitude,
                UpdatedDateUtc = Extensions.GetCurrentDateUtc(),
                CreatedDateUtc = district.CreatedDateUtc,
                CreatedUid = district.CreatedUid,
                UpdatedUid = userId
            };
        }

        public static WardUpdateCommand ToUpdateWardCommand(this LocationUpdateRequest ward, string userId)
        {
            if (ward == null)
                return null;
            return new WardUpdateCommand()
            {
                Id = ward.Id,
                DistrictId = ward.DistrictId,
                Prefix = ward.Prefix,
                WardName = ward.WardName,
                WardNameEN = ward.WardNameEN,
                Status = ward.Status,
                ShortName = ward.ShortName,
                Priority = ward.Priority,
                Latitude = ward.Latitude,
                Longitude = ward.Longitude,
                UpdatedDateUtc = Extensions.GetCurrentDateUtc(),
                CreatedDateUtc = ward.CreatedDateUtc,
                CreatedUid = ward.CreatedUid,
                UpdatedUid = userId
            };
        }

        public static LocationRemoveCommand ToCommandRemove(this LocationRemoveRequest command, string userId)
        {
            if (command == null)
                return null;
            return new LocationRemoveCommand()
            {
                ProvinceId = command.ProvinceId,
                DistricId = command.DistricId,
                WardId = command.WardId,
                StreetId = command.StreetId,
                UpdatedUid = userId,
                UpdatedDateUtc = Extensions.GetCurrentDateUtc(),
                TypeLocation = command.TypeLocation
            };
        }
    }
}
