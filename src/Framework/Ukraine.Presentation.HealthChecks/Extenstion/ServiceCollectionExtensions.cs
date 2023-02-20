using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ukraine.Presentation.HealthChecks.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IHealthChecksBuilder AddUkraineHealthChecks(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddHealthChecks();
	}

	public static IHealthChecksBuilder AddUkraineServiceCheck(this IHealthChecksBuilder builder)
	{
		return builder
			.AddCheck(Constants.DEFAULT_SERVICE_NAME, () => HealthCheckResult.Healthy(), tags: new[]
			{
				Constants.Tags.READY, Constants.DEFAULT_SERVICE_NAME
			});
	}
}