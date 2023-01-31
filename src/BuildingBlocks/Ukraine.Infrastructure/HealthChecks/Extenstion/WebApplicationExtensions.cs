using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ukraine.Infrastructure.HealthChecks.Extenstion;

public static class WebApplicationExtensions
{
    public static void UseCustomHealthChecks(this WebApplication webApplication)
    {
        webApplication.MapHealthChecks("/health/status", new HealthCheckOptions
        {
            Predicate = reg => reg.Tags.Contains("ready"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
    
    public static void UseCustomDatabaseHealthChecks(this WebApplication webApplication)
    {
        webApplication.MapHealthChecks("/health/database", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("database"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}