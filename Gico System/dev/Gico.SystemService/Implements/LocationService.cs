using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.AddressDataObject.Interfaces;
using Gico.AddressDomain;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.Domains;
using Gico.EsStorage;
using Gico.ExceptionDefine;
using Gico.ReadAddressModels;
using Gico.SystemCommands;
using Gico.SystemService.Interfaces;

namespace Gico.SystemService.Implements
{
    public class LocationService : ILocationService
    {
        //private readonly IEsStorage _esStorage;
        private readonly ILocationRepository _locationRepository;
        private readonly ICommandSender _commandService;

        public LocationService(ILocationRepository locationRepository, ICommandSender commandService)
        {
            _locationRepository = locationRepository;
            //_esStorage = esStorage;
            _commandService = commandService;
        }

        #region Old
        public async Task<bool> AddToEs(RProvince province, RDistrict district, RWard ward, RStreet street)
        {
            //RLocation location = CreateLocation(province, district, ward, street);
            //IndexEs<RLocation> indexEs = new IndexEs<RLocation>(EnumDefine.EsIndexName.AddressesBase, EnumDefine.EsIndexType.AddressBase, location.Key, location);
            //string response = await _esStorage.Add(indexEs);
            //var obj = Serialize.JsonDeserializeObject<EsAddResultDetail>(response);
            //return obj._shards.successful > 0 && obj._shards.failed <= 0;
            return false;
        }

        public async Task<KeyValuePair<string, bool>[]> AddToEs(Tuple<RProvince, RDistrict, RWard, RStreet>[] locations)
        {
            //RLocation[] locationsConvert =
            //    locations?.Select(p => CreateLocation(p.Item1, p.Item2, p.Item3, p.Item4)).ToArray();
            //if (locationsConvert != null && locationsConvert.Length > 0)
            //{
            //    IndexEs<RLocation>[] indexEses = new IndexEs<RLocation>[locationsConvert.Length];
            //    int i = 0;
            //    foreach (var location in locationsConvert)
            //    {
            //        indexEses[i] = new IndexEs<RLocation>(EnumDefine.EsIndexName.AddressesBase, EnumDefine.EsIndexType.AddressBase, location.Key, location);
            //        i++;
            //    }
            //    string response = await _esStorage.Add(indexEses);
            //    var obj = Serialize.JsonDeserializeObject<EsAddResult>(response);
            //    return obj?.items.Select(p => new KeyValuePair<string, bool>(p.index._id,
            //        (p.index._shards.successful >= 1 && p.index._shards.failed == 0))).ToArray();
            //}
            return null;
        }

        public async Task<RLocation[]> Search(string text)
        {
            //var response = await _esStorage.Search(EnumDefine.EsIndexName.AddressesBase, EnumDefine.EsIndexType.AddressBase, EsQuery(text));
            //var resultObject = Serialize.JsonDeserializeObject<EsSearchResult<RLocation>>(response);
            //return resultObject?.hits?.hits?.Select(p => p._source).ToArray();
            return null;
        }

        public static string EsQuery(string address)
        {
            string fieldName = "FullAddress";
            string query =
                 $"{{\"from\":0,\"size\":100,\"query\":{{\"bool\":{{\"must\":[{{\"match\":{{\"{fieldName}\":{{\"query\":\"{address}\"}}}}}}]}}}}}}";
            return query;
        }
        #endregion

        #region Get

        public async Task<RProvince[]> ProvinceGetAllFromDb(string name)
        {
            return await _locationRepository.ProvinceGetAll(name);
        }

        public async Task<RDistrict[]> DistrictGetByProvinceIdFromDb(string provinceId, string name)
        {
            return await _locationRepository.DistrictGetByProvinceId(provinceId, name);
        }

        public async Task<RWard[]> WardGetByDistrictIdFromDb(string districtId, string name)
        {
            return await _locationRepository.WardGetByDistrictId(districtId, name);
        }

        public async Task<RStreet[]> StreetGetByWardIdFromDb(string wardId)
        {
            return await _locationRepository.StreetGetByWardId(wardId);
        }

        public async Task<RProvince> GetProvinceById(string id)
        {
            return await _locationRepository.GetProvinceById(id);
        }

        public async Task<RDistrict> GetDistrictById(string id)
        {
            return await _locationRepository.GetDistrictById(id);
        }

        public async Task<RWard> GetWardById(string id)
        {
            return await _locationRepository.GetWardById(id);
        }

        public async Task<RStreet> GetStreetById(string id)
        {
            return await _locationRepository.GetStreetById(id);
        }

        #endregion

        #region CRUD

        public async Task ChangeToDb(Province province)
        {
            bool isChanged = await _locationRepository.Update(province);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Location_NotChanged);
            }
        }

        public async Task UpdateDistrict(District district)
        {
            bool isChanged = await _locationRepository.UpdateDistrict(district);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Location_NotChanged);
            }
        }

        public async Task UpdateWard(Ward ward)
        {
            bool isChanged = await _locationRepository.UpdateWard(ward);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Location_NotChanged);
            }
        }

        public async Task ChangeProvinceStatus(string id, EnumDefine.CommonStatusEnum status, string updatedUid, DateTime updatedDate)
        {
            await _locationRepository.ChangeProvinceStatus(id, status, updatedUid, updatedDate);
        }

        public async Task ChangeDistrictStatus(string id, EnumDefine.CommonStatusEnum status, string updatedUid, DateTime updatedDate)
        {
            await _locationRepository.ChangeDistrictStatus(id, status, updatedUid, updatedDate);
        }

        public async Task ChangeWardStatus(string id, EnumDefine.CommonStatusEnum status, string updatedUid, DateTime updatedDate)
        {
            await _locationRepository.ChangeWardStatus(id, status, updatedUid, updatedDate);
        }

        public async Task ChangeStreetStatus(string id, EnumDefine.CommonStatusEnum status, string updatedUid, DateTime updatedDate)
        {
            await _locationRepository.ChangeStreetStatus(id, status, updatedUid, updatedDate);
        }

        #endregion

        #region Sendcommand

        public async Task<CommandResult> SendCommand(ProvinceUpdateCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(DistrictUpdateCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(WardUpdateCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> SendCommand(LocationRemoveCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion

    }
}