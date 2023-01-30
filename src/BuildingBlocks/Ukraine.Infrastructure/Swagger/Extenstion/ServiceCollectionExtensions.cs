using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ukraine.Infrastructure.Swagger.Options;

namespace Ukraine.Infrastructure.Swagger.Extenstion;

public static class ServiceCollectionExtensions
{
    public static void AddCustomSwagger(this IServiceCollection serviceCollection, Action<CustomSwaggerOptions> options)
    {
        var opt = new CustomSwaggerOptions();
        options.Invoke(opt);
        
        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"Ukraine API - {opt.ApplicationName ?? "Application"}",
                Version = "v1"
            });
        });
    }
}