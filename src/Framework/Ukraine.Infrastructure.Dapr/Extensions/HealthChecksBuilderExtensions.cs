using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.Dapr.Extensions;

public static class HealthChecksBuilderExtensions
{
	public static IHealthChecksBuilder AddUkraineDaprHealthCheck(this IHealthChecksBuilder builder)
	{
		builder.AddCheck<HealthCheck>(Constants.SERVICE_NAME, tags: new[]
		{
			Constants.Tags.READY, Constants.SERVICE_NAME
		});

		return builder;
	}
}