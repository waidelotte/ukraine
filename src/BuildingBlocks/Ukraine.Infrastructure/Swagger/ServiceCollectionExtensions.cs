using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ukraine.Infrastructure.Swagger;

public static class ServiceCollectionExtensions
{
    public static void AddCustomSwagger(this IServiceCollection serviceCollection, string serviceName) =>
        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"Ukraine API - {serviceName}", Version = "v1" });
        });
}