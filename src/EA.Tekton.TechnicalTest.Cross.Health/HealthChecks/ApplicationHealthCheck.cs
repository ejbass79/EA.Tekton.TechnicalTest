using Microsoft.Extensions.Diagnostics.HealthChecks;

using System.Reflection;

namespace EA.Tekton.TechnicalTest.Cross.Health.HealthChecks;

public class ApplicationHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("EA.Tekton.TechnicalTest.WebApi");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
}
