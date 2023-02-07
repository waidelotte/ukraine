using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ukraine.Infrastructure.Swagger.Extenstion;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUkraineSwagger(this IServiceCollection serviceCollection, string serviceName)
    {
        return serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(Constants.DEFAULT_VERSION, new OpenApiInfo
            {
                Title = $"Ukraine REST API - {serviceName}",
                Version = Constants.DEFAULT_VERSION
            });
        });
    }
}