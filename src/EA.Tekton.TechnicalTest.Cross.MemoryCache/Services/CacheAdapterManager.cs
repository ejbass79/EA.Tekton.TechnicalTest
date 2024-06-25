using EA.Tekton.TechnicalTest.Cross.MemoryCache.Options;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace EA.Tekton.TechnicalTest.Cross.MemoryCache.Services;

public class CacheAdapterManager
{
    private readonly IMemoryCache _memoryCache;
    private readonly int _expirationTimeMinutes;

    public CacheAdapterManager(IMemoryCache memoryCache, IConfiguration configuration)
    {
        _memoryCache = memoryCache;

        var cacheOptions = new CacheOptions();

        configuration.GetSection(nameof(CacheOptions)).Bind(cacheOptions);

        _expirationTimeMinutes = cacheOptions.ExpirationTimeMinutes;
    }

    public T? GetFromCacheByKey<T>(string key)
    {
        return (T?)_memoryCache.Get(key);
    }

    public void AddToCache<T>(string key, T value)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_expirationTimeMinutes)
        };

        _memoryCache.Set(key, value, cacheEntryOptions);
    }
}