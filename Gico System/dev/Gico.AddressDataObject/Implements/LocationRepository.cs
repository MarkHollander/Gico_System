using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.AddressDataObject.Interfaces;
using Gico.AddressDomain;
using Gico.Common;
using Gico.Config;
using Gico.ReadAddressModels;

namespace Gico.AddressDataObject.Implements
{
    public class LocationRepository : SqlBaseDao, ILocationRepository
    {
        #region Read

        #region Province

        public async Task<RProvince[]> ProvinceGetAll(string name)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProvinceName", name, DbType.String); 
            parameters.Add("@ProvinceNameEN", name, DbType.String);
            var data = await WithConnection(async (connection) => await connection.QueryAsync<RProvince>(ProcName.Province_GetAll, parameters, commandType: CommandType.StoredProcedure));
            return data.ToArray();
        }

        public async Task<RProvince> GetProvinceById(string id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RProvince>(ProcName.Province_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        #endregion

        #region District

        public async Task<RDistrict[]> DistrictGetByProvinceId(string provinceId, string name)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProvinceId", provinceId, DbType.Int32);
                parameters.Add("@DistrictName", name, DbType.String);
                return await connection.QueryAsync<RDistrict>(ProcName.District_GetByProvinceId, parameters, commandType: CommandType.StoredProcedure);
            });
            return data.ToArray();
        }

        public async Task<RDistrict> GetDistrictById(string id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RDistrict>(ProcName.District_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        #endregion

        #region Ward
        public async Task<RWard[]> WardGetByDistrictId(string districtId, string name)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DistrictId", districtId, DbType.Int32);
                parameters.Add("@WardName", name, DbType.String);
                return await connection.QueryAsync<RWard>(ProcName.Ward_GetByDistrictId, parameters, commandType: CommandType.StoredProcedure);
            });
            return data.ToArray();
        }

        public async Task<RWard> GetWardById(string id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RWard>(ProcName.Ward_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        } 
        #endregion

        #region Street

        public async Task<RStreet[]> StreetGetByWardId(string wardId)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@WardId", wardId, DbType.String);
                return await connection.QueryAsync<RStreet>(ProcName.Street_GetByWardId, parameters, commandType: CommandType.StoredProcedure);
            });
            return data.ToArray();
        }

        public async Task<RStreet> GetStreetById(string id)
        {
            var data = await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RStreet>(ProcName.Street_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
            return data;
        }

        #endregion

        #endregion

        #region Write

        public async Task<bool> Update(Province province)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", province.Id, DbType.String);
                parameters.Add("@Prefix", province.Prefix, DbType.String);
                parameters.Add("@ProvinceName", province.ProvinceName, DbType.String);
                parameters.Add("@ProvinceNameEN", province.ProvinceNameEN, DbType.String);
                parameters.Add("@Status", province.Status, DbType.Int64);
                parameters.Add("@ShortName", province.ShortName, DbType.String);
                parameters.Add("@Priority", province.Priority, DbType.Int64);
                parameters.Add("@Latitude", province.Latitude, DbType.Int64);
                parameters.Add("@Longitude", province.Longitude, DbType.Int64);
                parameters.Add("@RegionId", province.RegionId, DbType.Int64);
                parameters.Add("@CreatedDateUtc", province.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", province.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", province.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", province.UpdatedUid, DbType.String);
                
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Province_Update, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;

            });
        }

        public async Task<bool> UpdateDistrict(District district)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", district.Id, DbType.String);
                parameters.Add("@ProvinceId", district.ProvinceId, DbType.String);
                parameters.Add("@Prefix", district.Prefix, DbType.String);
                parameters.Add("@DistrictName", district.DistrictName, DbType.String);
                parameters.Add("@DistrictNameEN", district.DistrictNameEN, DbType.String);
                parameters.Add("@Status", district.Status, DbType.Int64);
                parameters.Add("@ShortName", district.ShortName, DbType.String);
                parameters.Add("@Priority", district.Priority, DbType.Int64);
                parameters.Add("@Latitude", district.Latitude, DbType.Int64);
                parameters.Add("@Longitude", district.Longitude, DbType.Int64);
                parameters.Add("@CreatedDateUtc", district.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", district.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", district.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", district.UpdatedUid, DbType.String);

                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.District_Update, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;

            });
        }

        public async Task<bool> UpdateWard(Ward ward)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", ward.Id, DbType.String);
                parameters.Add("@DistrictId", ward.DistrictId, DbType.String);
                parameters.Add("@Prefix", ward.Prefix, DbType.String);
                parameters.Add("@WardName", ward.WardName, DbType.String);
                parameters.Add("@WardNameEN", ward.WardNameEN, DbType.String);
                parameters.Add("@Status", ward.Status, DbType.Int64);
                parameters.Add("@ShortName", ward.ShortName, DbType.String);
                parameters.Add("@Priority", ward.Priority, DbType.Int64);
                parameters.Add("@Latitude", ward.Latitude, DbType.Int64);
                parameters.Add("@Longitude", ward.Longitude, DbType.Int64);
                parameters.Add("@CreatedDateUtc", ward.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", ward.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", ward.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", ward.UpdatedUid, DbType.String);

                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Ward_Update, parameters, commandType: CommandType.StoredProcedure);
                return rowCount == 1;

            });
        }

        public async Task ChangeProvinceStatus(string id, EnumDefine.CommonStatusEnum provinceStatus, string updatedUid, DateTime updatedDate)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@Status", provinceStatus.AsEnumToInt(), DbType.Int16);
                parameters.Add("@UpdatedDateUtc", updatedDate, DbType.DateTime);
                parameters.Add("@UpdatedUid", updatedUid, DbType.String);
                return await connection.ExecuteAsync(ProcName.Province_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task ChangeDistrictStatus(string id, EnumDefine.CommonStatusEnum districtStatus, string updatedUid, DateTime updatedDate)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@Status", districtStatus.AsEnumToInt(), DbType.Int16);
                parameters.Add("@UpdatedDateUtc", updatedDate, DbType.DateTime);
                parameters.Add("@UpdatedUid", updatedUid, DbType.String);
                return await connection.ExecuteAsync(ProcName.District_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task ChangeWardStatus(string id, EnumDefine.CommonStatusEnum districtStatus, string updatedUid, DateTime updatedDate)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@Status", districtStatus.AsEnumToInt(), DbType.Int16);
                parameters.Add("@UpdatedDateUtc", updatedDate, DbType.DateTime);
                parameters.Add("@UpdatedUid", updatedUid, DbType.String);
                return await connection.ExecuteAsync(ProcName.Ward_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task ChangeStreetStatus(string id, EnumDefine.CommonStatusEnum districtStatus, string updatedUid, DateTime updatedDate)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@Status", districtStatus.AsEnumToInt(), DbType.Int16);
                parameters.Add("@UpdatedDateUtc", updatedDate, DbType.DateTime);
                parameters.Add("@UpdatedUid", updatedUid, DbType.String);
                return await connection.ExecuteAsync(ProcName.Street_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        #endregion
    }
}