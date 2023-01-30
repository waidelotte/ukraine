using Microsoft.AspNetCore.Builder;

namespace Ukraine.Infrastructure.Swagger.Extenstion;

public static class WebApplicationExtensions
{
    public static void UseCustomSwagger(this WebApplication application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        });
    }
}