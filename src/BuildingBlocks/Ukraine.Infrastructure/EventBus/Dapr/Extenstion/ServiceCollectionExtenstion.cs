using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EventBus.Dapr.Extenstion;

public static class DaprServiceCollectionExtenstion
{
    public static IServiceCollection AddCustomDapr(this IServiceCollection services)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        services.AddDaprClient(client =>
        {
            client.UseJsonSerializationOptions(options);
        });
            
        services.AddScoped<IEventBus, DaprEventBus>();

        return services;
    }
}