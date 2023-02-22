using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Dapr.Extensions;

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