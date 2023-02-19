using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Telemetry.Options;

namespace Ukraine.Infrastructure.Telemetry.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineTelemetry(
		this IServiceCollection services,
		string serviceName,
		Action<UkraineTelemetryOptionsBuilder>? configure = null)
	{
		var options = new UkraineTelemetryOptionsBuilder(serviceName);
		configure?.Invoke(options);

		services.AddOpenTelemetryTracing(options.Build());

		return services;
	}
}