﻿using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Domain.Interfaces;

namespace Ukraine.Infrastructure.Dapr.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineDapr(this IServiceCollection services)
	{
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = Constants.Serialization.CaseInsensitive,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		services.AddDaprClient(client =>
		{
			client.UseJsonSerializationOptions(options);
		});

		services.AddScoped<IEventBus, EventBus>();

		return services;
	}
}