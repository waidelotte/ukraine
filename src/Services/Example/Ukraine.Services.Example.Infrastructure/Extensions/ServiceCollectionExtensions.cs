using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.EventBus.Extensions;
using Ukraine.Core.Mediator.Extensions;
using Ukraine.Telemetry.Extenstion;
using Ukraine.HealthChecks.Extenstion;

namespace Ukraine.Services.Example.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddUkraineMediatorAndValidators(Assembly.GetExecutingAssembly());
		services.AddUkraineMediatorRequestValidation();
		services.AddUkraineTelemetry(configuration.GetSection("UkraineTelemetry"));
		services.AddUkraineDaprEventBus(configuration.GetSection("UkraineEventBus"));
		services.AddUkraineHealthChecks().AddUkraineDaprHealthCheck().AddUkraineServiceCheck();

		return services;
	}
}