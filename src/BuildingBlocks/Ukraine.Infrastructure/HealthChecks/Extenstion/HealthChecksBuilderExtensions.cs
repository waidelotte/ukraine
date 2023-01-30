using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.HealthChecks.Extenstion;

public static class HealthChecksBuilderExtensions
{
    
    public static IHealthChecksBuilder AddCustomNpgSql(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
    {
        return healthChecksBuilder
            .AddNpgSql(connectionString, name: "Database Npg", tags: new []{ "ready", "database", "npg"});
    }
}