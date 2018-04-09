using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Gico.Caching.Redis;
using Gico.Common;
using Gico.Config;
using Gico.DataObject;
using Gico.ReadSystemModels;
using Gico.ShardingDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;

namespace Gico.SystemService.Implements
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IRedisStorage _redisStorage;
        private readonly IShardingConfigRepository _shardingConfigDao;
        public CommonService(ICommonRepository commonRepository, IRedisStorage redisStorage, IShardingConfigRepository shardingConfigDao)
        {
            _commonRepository = commonRepository;
            _redisStorage = redisStorage;
            _shardingConfigDao = shardingConfigDao;
        }
        public async Task<long> GetNextId(Type objType)
        {
            string pathName = objType.Name;
            try
            {
                long nextValue = await _commonRepository.GetNextValueForSequence(pathName);
                return nextValue;
            }
            catch (SqlException e)
            {
                if (e.Message.StartsWith("Invalid object name 'Sequence"))
                {
                    await _commonRepository.CreateSequence(pathName);
                    long nextValue = await _commonRepository.GetNextValueForSequence(pathName);
                    return nextValue;
                }
                throw;
            }
            catch (Exception e)
            {
                e.ExceptionAddParam("CommonService.GetNextId", objType);
                throw;
            }

        }
        public async Task<string> GetNextCode(Type objType)
        {
            long id = await GetNextId(objType);
            string code = Common.Common.GenerateCodeFromId(id, 3);
            return code;
        }
        public Task<string> GetNextId()
        {
            return Task.FromResult(Common.Common.GenerateGuid());
        }

        public async Task SetFlagRegisterSuccessAsync(string id)
        {
            string key = string.Format(SystemDefine.FlagRegisterSuccessKey, id);
            await _redisStorage.StringSet(key, true, TimeSpan.FromMinutes(2));
        }

        public async Task<bool> GetFlagRegisterSuccessAsync(string id)
        {
            string key = string.Format(SystemDefine.FlagRegisterSuccessKey, id);
            return await _redisStorage.KeyDelete(key);
        }

        public async Task<RShardingConfig[]> Get(EnumDefine.ShardGroupEnum shardGroup)
        {
            return await _shardingConfigDao.Get(shardGroup);
        }

        private string ShardingConfigKey(EnumDefine.ShardGroupEnum shardGroup)
        {
            return $"ShardingConfigKey:{shardGroup}";
        }
        public async Task<RShardingConfig[]> ShardingConfigGetFromCache(EnumDefine.ShardGroupEnum shardGroup)
        {
            string key = ShardingConfigKey(shardGroup);
            RShardingConfig[] configs = await _redisStorage.HashGetAll<RShardingConfig>(key);
            if (configs == null)
            {
                var configsFromDb = await _shardingConfigDao.Get(shardGroup);
                configs = configsFromDb?.Select(p => new RShardingConfig()
                {
                    Id = p.Id,
                    Type = p.Type,
                    Status = p.Status,
                    UpdatedUid = p.UpdatedUid,
                    CreatedUid = p.CreatedUid,
                    Config = p.Config,
                    CreatedDate = p.CreatedDate,
                    DatabaseName = p.DatabaseName,
                    HostName = p.HostName,
                    Pwd = p.Pwd,
                    ShardGroup = p.ShardGroup,
                    ShardGroupName = p.ShardGroupName,
                    StatusName = p.StatusName,
                    TypeName = p.TypeName,
                    Uid = p.Uid,
                    UpdatedDate = p.UpdatedDate
                }).ToArray();
            }
            return configs;
        }
    }
}