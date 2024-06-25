using EA.Tekton.TechnicalTest.Cross.MemoryCache.Options;
using EA.Tekton.TechnicalTest.Cross.MemoryCache.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EA.Tekton.TechnicalTest.Cross.MemoryCache;

public static class DependencyInjection
{
    public static IServiceCollection AddMemoryCacheService(this IServiceCollection services)
    {
        // DI MemoryCache
        services.AddMemoryCache();
        
        // Agregar Clases
        services.AddSingleton<CacheAdapterManager>();
        services.AddSingleton<CacheService>();

        return services;
    }
}
