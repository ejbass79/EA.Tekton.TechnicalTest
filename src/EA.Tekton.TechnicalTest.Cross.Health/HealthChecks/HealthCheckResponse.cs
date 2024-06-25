namespace EA.Tekton.TechnicalTest.Cross.Health.HealthChecks;

public class HealthCheckResponse
{
    public string OverallStatus { get; set; } = string.Empty;
    public IEnumerable<HealthCheckItem> HealthChecks { get; set; } = Enumerable.Empty<HealthCheckItem>();
    public string TotalDuration { get; set; } = string.Empty;
}
