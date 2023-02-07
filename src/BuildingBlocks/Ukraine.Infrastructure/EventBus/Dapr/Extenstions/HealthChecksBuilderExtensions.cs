using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.EventBus.Dapr.Extenstions;

public static class HealthChecksBuilderExtensions
{
	public static IHealthChecksBuilder AddUkraineDaprHealthCheck(this IHealthChecksBuilder builder)
	{
		builder.AddCheck<HealthCheck>(Constants.SERVICE_NAME, tags: new []
		{
			HealthChecks.Constants.Tags.READY, Constants.SERVICE_NAME
		});

		return builder;
	}
}