using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.EventBus.Extensions;

public static class HealthChecksBuilderExtensions
{
	public static IHealthChecksBuilder AddUkraineDaprHealthCheck(this IHealthChecksBuilder builder)
	{
		return builder.AddCheck<DaprHealthCheck>("dapr", tags: new[]
		{
			"ready", "dapr"
		});
	}
}