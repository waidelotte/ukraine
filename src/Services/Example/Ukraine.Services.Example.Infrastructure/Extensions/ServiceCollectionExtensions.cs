using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EventBus.Dapr.Extensions;
using Ukraine.Infrastructure.Mediator.Extensions;
using Ukraine.Infrastructure.Telemetry.Extenstion;
using Ukraine.Infrastructure.Telemetry.Options;

namespace Ukraine.Services.Example.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		string serviceName,
		Action<UkraineTelemetryOptions>? telemetryOptions = null)
	{
		services.AddUkraineMediatorAndValidators(Assembly.GetExecutingAssembly());
		services.AddUkraineMediatorRequestValidation();

		services
			.AddUkraineTelemetry(serviceName, telemetryOptions)
			.AddUkraineDaprEventBus();

		return services;
	}
}