using System;
using Gico.AddressDomain;
using Gico.ReadAddressModels;
using System.Threading.Tasks;
using Gico.Config;

namespace Gico.AddressDataObject.Interfaces
{
    public interface ILocationRepository
    {
        #region Province

        Task<RProvince[]> ProvinceGetAll(string name);
        Task<bool> Update(Province province);
        Task<RProvince> GetProvinceById(string id);
        Task ChangeProvinceStatus(string id, EnumDefine.CommonStatusEnum provinceStatus, string updatedUid, DateTime updatedDate);

        #endregion

        #region Districs
        Task<RDistrict[]> DistrictGetByProvinceId(string provinceId, string name);
        Task<RDistrict> GetDistrictById(string id);
        Task<bool> UpdateDistrict(District district);
        Task ChangeDistrictStatus(string id, EnumDefine.CommonStatusEnum provinceStatus, string updatedUid, DateTime updatedDate);
        #endregion

        #region Ward
        Task<RWard[]> WardGetByDistrictId(string districtId, string name);
        Task<RWard> GetWardById(string id);
        Task<bool> UpdateWard(Ward ward);
        Task ChangeWardStatus(string id, EnumDefine.CommonStatusEnum provinceStatus, string updatedUid, DateTime updatedDate);
        #endregion

        #region Street
        Task<RStreet[]> StreetGetByWardId(string wardId);
        Task<RStreet> GetStreetById(string id);
        Task ChangeStreetStatus(string id, EnumDefine.CommonStatusEnum provinceStatus, string updatedUid, DateTime updatedDate);
        #endregion
    }
}