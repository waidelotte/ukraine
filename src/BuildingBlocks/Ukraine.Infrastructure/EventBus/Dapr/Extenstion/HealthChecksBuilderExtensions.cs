using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.EventBus.Dapr.Extenstion;

public static class HealthChecksBuilderExtensions
{
	public static IHealthChecksBuilder AddCustomDaprHealthCheck(this IHealthChecksBuilder builder)
	{
		builder.AddCheck<DaprHealthCheck>("Dapr", tags: new []{ "ready", "dapr"});

		return builder;
	}
}