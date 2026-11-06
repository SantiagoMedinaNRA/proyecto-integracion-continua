namespace Api.Brokers;

public interface ICacheBroker
{
    ValueTask<T?> GetAsync<T>(string key) where T : class;
    ValueTask SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class;
    ValueTask RemoveAsync(string key);
}
