using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ukraine.Infrastructure.HealthChecks;

public static class ServiceCollectionExtensions
{
	public static IHealthChecksBuilder AddCustomHealthChecks(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddHealthChecks()
			.AddCheck("Application", () => HealthCheckResult.Healthy(), tags: new[] { "ready", "application" });
	}
}