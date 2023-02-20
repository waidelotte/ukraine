using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ukraine.Presentation.Swagger.Options;

namespace Ukraine.Presentation.Swagger.Extenstion;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseUkraineSwagger(this WebApplication application)
	{
		var options = application.Services.GetRequiredService<IOptions<UkraineSwaggerOptions>>();

		application.UseSwagger();
		return application.UseSwaggerUI(o =>
		{
			o.SwaggerEndpoint(options.Value.Endpoint, options.Value.EndpointName);

			if (options.Value.OAuth2Security != null)
			{
				o.OAuthClientId(options.Value.OAuth2Security.ClientId);
				o.OAuthAppName(options.Value.OAuth2Security.ClientName);
			}
		});
	}
}