namespace Api.Brokers;

using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

public class CacheBroker : ICacheBroker
{
    private readonly IDistributedCache distributedCache;
    private readonly ILogger<CacheBroker> logger;

    public CacheBroker(IDistributedCache distributedCache, ILogger<CacheBroker> logger)
    {
        this.distributedCache = distributedCache;
        this.logger = logger;
    }

    public async ValueTask<T?> GetAsync<T>(string key) where T : class
    {
        try
        {
            var json = await distributedCache.GetStringAsync(key);
            
            if (!string.IsNullOrEmpty(json))
            {
                logger.LogInformation("[CACHE HIT] Retrieved '{Key}' from Valkey", key);
                return JsonSerializer.Deserialize<T>(json);
            }

            logger.LogInformation("[CACHE MISS] '{Key}' not found in Valkey", key);
            return null;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "[CACHE ERROR] Failed to get '{Key}' from Valkey", key);
            return null;
        }
    }

    public async ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        try
        {
            var json = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromHours(1)
            };

            await distributedCache.SetStringAsync(key, json, options);
            logger.LogInformation("[STORED IN VALKEY] '{Key}' cached for {Hours}h", key, (expiration?.TotalHours ?? 1).ToString("F1"));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "[CACHE ERROR] Failed to set '{Key}' in Valkey", key);
        }
    }

    public async ValueTask RemoveAsync(string key)
    {
        try
        {
            await distributedCache.RemoveAsync(key);
            logger.LogInformation("[CACHE REMOVED] '{Key}' removed from Valkey", key);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "[CACHE ERROR] Failed to remove '{Key}' from Valkey", key);
        }
    }
}
