using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.Dapr;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDaprEventBus(
		this IServiceCollection services,
		Action<DaprEventBusOptions> configure)
	{
		AddDaprClient(services);

		services.Configure(configure);
		services.TryAddScoped<IEventBus, DaprEventBus>();

		return services;
	}

	public static IServiceCollection AddDaprEmailService(
		this IServiceCollection services,
		Action<DaprEmailOptions> configure)
	{
		AddDaprClient(services);

		services.Configure(configure);
		services.TryAddScoped<IEmailService, DaprEmailService>();

		return services;
	}

	private static void AddDaprClient(IServiceCollection services)
	{
		var serializerOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		services.AddDaprClient(client =>
		{
			client.UseJsonSerializationOptions(serializerOptions);
		});
	}
}