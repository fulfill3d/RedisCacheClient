using Client.Interfaces;
using Client.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Client
{
    public class RedisCacheClient(IConnectionMultiplexer connectionMultiplexer, IOptions<RedisCacheClientOptions> opt): IRedisCacheClient
    {
        private readonly IDatabaseAsync _database = connectionMultiplexer.GetDatabase();
        private readonly RedisCacheClientOptions _redisCacheClientOptions = opt.Value;

        public async Task<T> GetCacheObjectAsync<T>(string key) 
        {
            string value = await _database.StringGetAsync(key);

            if(value == null) return default;

            return JsonConvert.DeserializeObject<T>(value);
        }

        
        public async Task<bool> SetCacheObjectAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            return await _database.StringSetAsync(
                key,
                JsonConvert.SerializeObject(value),
                expiry ?? TimeSpan.FromDays(_redisCacheClientOptions.DefaultStringExpiryDay));
        }


        public async Task<T> GetCacheObjectAsync<T>(string key, string field)
        {
            string value = await _database.HashGetAsync(key,field);

            if (value == null) return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<bool> SetCacheObjectAsync<T>(string key, string field, T value, TimeSpan? expiry = null)
        {
            bool keyExists = await _database.KeyExistsAsync(key);

            var result = await _database.HashSetAsync(
                key,
                field,
                JsonConvert.SerializeObject(value));

            if (!keyExists && result)
            {
                await _database.KeyExpireAsync(key, expiry ?? TimeSpan.FromDays(_redisCacheClientOptions.DefaultHashExpiryDay));
            }

            return result;
        }

        public async Task<bool> DeleteCacheObjectAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> DeleteCacheObjectAsync(string key, string hashField)
        {
            return await _database.HashDeleteAsync(key, hashField);
        }
    }
}