using EA.Tekton.TechnicalTest.Cross.Configuration;
using EA.Tekton.TechnicalTest.Cross.Health.HealthChecks;

using Microsoft.Extensions.DependencyInjection;

namespace EA.Tekton.TechnicalTest.Cross.Health;

public static class DependencyInjection
{
    public static IServiceCollection AddCrossHealthService(this IServiceCollection services)
    {
        // Register Application Health Checks
        services.AddHealthChecks().AddCheck<ApplicationHealthCheck>(name: CrossConfiguration.ApplicationName);

        return services;
    }
}
