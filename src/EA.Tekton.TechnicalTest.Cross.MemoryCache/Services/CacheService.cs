namespace EA.Tekton.TechnicalTest.Cross.MemoryCache.Services;

public class CacheService
{
    private readonly CacheAdapterManager _cacheManager;

    public CacheService(CacheAdapterManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    public T? GetFromCacheByKey<T>(string key)
    {
        return _cacheManager.GetFromCacheByKey<T?>(key);
    }

    public void AddToCache<T>(string key, T value)
    {
        _cacheManager.AddToCache(key, value);
    }
}
