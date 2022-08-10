using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ukraine.Infrastructure.HealthChecks;

public static class WebApplicationExtensions
{
    public static void UseCustomHealthChecks(this WebApplication webApplication)
    {
        webApplication.MapHealthChecks("/health/status", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        webApplication.MapHealthChecks("/health/database", new HealthCheckOptions
        {
            Predicate = r => r.Name.Contains("db"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}