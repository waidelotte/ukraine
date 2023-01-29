using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.EventBus.Dapr
{
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

            return services;
        }
    }
}
