namespace Client.Interfaces
{
    public interface IRedisCacheClient
    {
        Task<T> GetCacheObjectAsync<T>(string key);
        Task<bool> SetCacheObjectAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T> GetCacheObjectAsync<T>(string key, string field);
        Task<bool> SetCacheObjectAsync<T>(string key, string field, T value, TimeSpan? expiry = null);
        Task<bool> DeleteCacheObjectAsync(string key);
        Task<bool> DeleteCacheObjectAsync(string key, string hashField);
    }
}