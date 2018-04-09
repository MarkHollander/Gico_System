using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.AddressDomain;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadAddressModels;
using Gico.SystemCommands;

namespace Gico.SystemService.Interfaces
{
    public interface ILocationService
    {
        #region Get

        Task<RProvince[]> ProvinceGetAllFromDb(string name);
        Task<RDistrict[]> DistrictGetByProvinceIdFromDb(string provinceId, string name);
        Task<RWard[]> WardGetByDistrictIdFromDb(string districtId, string name);
        Task<RStreet[]> StreetGetByWardIdFromDb(string wardId);
        Task<RProvince> GetProvinceById(string id);
        Task<RDistrict> GetDistrictById(string id);
        Task<RWard> GetWardById(string id);
        Task<RStreet> GetStreetById(string id);

        #endregion

        #region CRUD

        Task ChangeToDb(Province province);
        Task UpdateDistrict(District district);
        Task UpdateWard(Ward ward);
        Task ChangeProvinceStatus(string id, EnumDefine.CommonStatusEnum bannerStatus, string updatedUid, DateTime updatedDate);
        Task ChangeDistrictStatus(string id, EnumDefine.CommonStatusEnum bannerStatus, string updatedUid, DateTime updatedDate);
        Task ChangeWardStatus(string id, EnumDefine.CommonStatusEnum bannerStatus, string updatedUid, DateTime updatedDate);
        Task ChangeStreetStatus(string id, EnumDefine.CommonStatusEnum bannerStatus, string updatedUid, DateTime updatedDate);

        #endregion

        #region SendCommand

        Task<CommandResult> SendCommand(ProvinceUpdateCommand command);
        Task<CommandResult> SendCommand(DistrictUpdateCommand command);
        Task<CommandResult> SendCommand(WardUpdateCommand command);
        Task<CommandResult> SendCommand(LocationRemoveCommand command);

        #endregion

        Task<bool> AddToEs(RProvince province, RDistrict district, RWard ward, RStreet street);
        Task<KeyValuePair<string, bool>[]> AddToEs(Tuple<RProvince, RDistrict, RWard, RStreet>[] locations);
        Task<RLocation[]> Search(string text);
    }
}