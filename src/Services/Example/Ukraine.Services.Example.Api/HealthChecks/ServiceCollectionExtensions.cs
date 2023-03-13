using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ukraine.Framework.Core;
using Ukraine.Framework.Core.HealthChecks;
using Ukraine.Framework.Dapr;

namespace Ukraine.Services.Example.Api.HealthChecks;

internal static class ServiceCollectionExtensions
{
	public static IServiceCollection ConfigureHealthChecks(
		this IServiceCollection serviceCollection,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetRequiredConnectionString("Postgres");

		serviceCollection
			.AddHealthChecks()
			.AddDefaultCheck("Service", new[] { "service", "api", "demo" })
			.AddNpgSql(
				connectionString,
				name: "Postgres Database",
				tags: new[] { "database", "postgres" })
			.AddDaprHealthCheck(
				"Dapr Sidecar",
				HealthStatus.Unhealthy,
				new[] { "service", "dapr" });

		return serviceCollection;
	}
}