using EA.Tekton.TechnicalTest.Cross.Crypto.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Crypto.Services;

using Microsoft.Extensions.DependencyInjection;

namespace EA.Tekton.TechnicalTest.Cross.Crypto;

public static class DependencyInjection
{
    public static IServiceCollection AddCrossCryptoService(this IServiceCollection services)
    {
        // Register Cross Crypto
        services.AddDataProtection();

        services.AddSingleton<IEncryptionService, EncryptionService>();

        return services;
    }
}
