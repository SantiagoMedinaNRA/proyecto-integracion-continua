namespace Api.Brokers;

public class CacheBroker : ICacheBroker
{
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<CacheBroker> logger;

    public CacheBroker(IMemoryCache memoryCache, ILogger<CacheBroker> logger)
    {
        this.memoryCache = memoryCache;
        this.logger = logger;
    }

    public ValueTask<T?> GetAsync<T>(string key) where T : class
    {
        if (memoryCache.TryGetValue(key, out T? value))
        {
            logger.LogInformation("Cache hit for key {Key}", key);
            return new ValueTask<T?>(value);
        }

        logger.LogInformation("Cache miss for key {Key}", key);
        return new ValueTask<T?>((T?)null);
    }

    public ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        var cacheOptions = new MemoryCacheEntryOptions();

        if (expiration.HasValue)
        {
            cacheOptions.AbsoluteExpirationRelativeToNow = expiration;
        }
        else
        {
            cacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
        }

        memoryCache.Set(key, value, cacheOptions);
        logger.LogInformation("Cache set for key {Key} with expiration {Expiration}", key, expiration?.TotalMinutes ?? 60);

        return new ValueTask();
    }

    public ValueTask RemoveAsync(string key)
    {
        memoryCache.Remove(key);
        logger.LogInformation("Cache removed for key {Key}", key);
        return new ValueTask();
    }
}
