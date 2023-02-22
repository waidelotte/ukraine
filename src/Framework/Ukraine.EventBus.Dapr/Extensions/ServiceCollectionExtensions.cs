using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ukraine.Domain.Interfaces;
using Ukraine.EventBus.Dapr.Options;

namespace Ukraine.EventBus.Dapr.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineDaprEventBus(
		this IServiceCollection services,
		IConfigurationSection configurationSection)
	{
		services.AddOptions<UkraineDaprOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var serializerOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		services.AddDaprClient(client =>
		{
			client.UseJsonSerializationOptions(serializerOptions);
		});

		services.TryAddScoped<IEventBus, DaprEventBus>();

		return services;
	}
}