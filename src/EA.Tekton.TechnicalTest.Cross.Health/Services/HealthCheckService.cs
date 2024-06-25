using EA.Tekton.TechnicalTest.Cross.Health.HealthChecks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Newtonsoft.Json;

using System.Net.Mime;

namespace EA.Tekton.TechnicalTest.Cross.Health.Services;

public static class HealthCheckService
{
    public static async Task WriterHealthCheckResponse(HttpContext httpContext, HealthReport report)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        var response = new HealthCheckResponse
        {
            OverallStatus = $"{report.Status}",
            TotalDuration = report.TotalDuration.TotalSeconds.ToString("0:0.00"),
            HealthChecks = report.Entries.Select(x => new HealthCheckItem
            {
                Status = x.Value.Status.ToString(),
                Component = x.Key,
                Description = x.Value.Description ?? string.Empty,
                Duration = x.Value.Duration.TotalSeconds.ToString("0:0.00")
            })
        };

        await httpContext.Response.WriteAsync(text: JsonConvert.SerializeObject(response, Formatting.Indented));
    }
}
