using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Gico.Common;

namespace Gico.Caching.Redis
{
    public interface IRedisStorage
    {
        Task<bool> LockTake(string key, string value, TimeSpan timeout);
        Task<bool> LockRelease(string key, string value);
        Task<bool> StringSet(string key, object value);
        Task<bool> StringSet(string key, object value, TimeSpan? timeout);
        Task<bool> KeyDelete(string key);
        Task<bool> HashExists(string key, string field);
        Task<T> StringGet<T>(string key);
        Task<long> ListLeftPush(string key, object value);
        Task<bool> HashSet(string key, string field, object value);
        Task<T[]> HashGetAll<T>(string key);
        Task<T> HashGet<T>(string key, string field);
        Task<T[]> HashGet<T>(string key, string[] fields);
        Task<bool> HashDelete(string key, string field);
        Task<bool> KeyExist(string key);
    }
    public class RedisStorage : IRedisStorage
    {
        private readonly IDatabase _writeDatabase;
        private readonly IDatabase _readDatabase;

        public RedisStorage(int dbId = 1)
        {
            _writeDatabase = RedisConnection.GetCurrentWriteConnection(dbId);
            _readDatabase = RedisConnection.GetCurrentReadConnection(dbId);
        }

        public async Task<bool> LockTake(string key, string value, TimeSpan timeout)
        {
            return await _writeDatabase.LockTakeAsync(key, value, timeout);
        }
        public async Task<bool> LockRelease(string key, string value)
        {
            return _writeDatabase.LockRelease(key, value);
        }

        public async Task<bool> StringSet(string key, object value)
        {
            RedisValue redisValue = ConvertInput(value);
            return await _writeDatabase.StringSetAsync(key, redisValue, null);
        }

        public async Task<bool> StringSet(string key, object value, TimeSpan? timeout)
        {
            RedisValue redisValue = ConvertInput(value);
            return await _writeDatabase.StringSetAsync(key, redisValue, timeout.Value);
        }

        public async Task<bool> KeyDelete(string key)
        {
            return await _writeDatabase.KeyDeleteAsync(key);
        }

        public async Task<T> StringGet<T>(string key)
        {
            RedisValue value = await _readDatabase.StringGetAsync(key);
            return ConvertOutput<T>(value);
        }

        public async Task<long> ListLeftPush(string key, object value)
        {
            return await _writeDatabase.ListLeftPushAsync(key, ConvertInput(value));
        }
        public async Task<bool> HashSet(string key, string field, object value)
        {
            RedisValue redisValue = ConvertInput(value);
            return await _writeDatabase.HashSetAsync(key, field, redisValue);
        }
        public async Task<T[]> HashGetAll<T>(string key)
        {
            HashEntry[] hashEntries = await _readDatabase.HashGetAllAsync(key);
            if (hashEntries.Length > 0)
            {
                T[] results = new T[hashEntries.Length];
                int i = 0;
                foreach (var hashEntry in hashEntries)
                {
                    results[i] = ConvertOutput<T>(hashEntry.Value);
                    i++;
                }
                return results;
            }
            return default(T[]);
        }
        public async Task<T> HashGet<T>(string key, string field)
        {
            RedisValue redisValue = await _readDatabase.HashGetAsync(key, field);
            return ConvertOutput<T>(redisValue);
        }

        public async Task<T[]> HashGet<T>(string key, string[] fields)
        {
            RedisValue[] redisValues = fields.Select(p => (RedisValue)p).ToArray();
            var values = await _readDatabase.HashGetAsync(key, redisValues);
            T[] results = new T[values.Length];
            int i = 0;
            foreach (var redisValue in values)
            {
                if (redisValue.HasValue)
                {
                    var obj = ConvertOutput<T>(redisValue);
                    results[i] = obj;
                }
                i++;
            }
            return results;
        }

        public async Task<bool> HashDelete(string key, string field)
        {
            return await _writeDatabase.HashDeleteAsync(key, field);
        }

        public async Task<bool> KeyExist(string key)
        {
            return _readDatabase.KeyExists(key);
        }

        public async Task<bool> HashExists(string key, string field)
        {
            return await _writeDatabase.HashExistsAsync(key, field);
        }
        private byte[] ConvertInput(object value)
        {
            return Serialize.ProtoBufSerialize(value);
        }
        private T ConvertOutput<T>(RedisValue redisValue)
        {
            if (redisValue.HasValue)
            {
                return Serialize.ProtoBufDeserialize<T>(redisValue);
            }
            return default(T);
        }
    }
}