namespace EA.Tekton.TechnicalTest.Cross.Options;

public class ApplicationInsightsOptions
{
    public bool Enabled { get; set; } = false;
    public string ConnectionString { get; set; } = string.Empty;
}
