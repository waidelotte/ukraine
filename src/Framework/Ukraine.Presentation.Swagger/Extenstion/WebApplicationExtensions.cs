using Microsoft.AspNetCore.Builder;
using Ukraine.Presentation.Swagger.Options;

namespace Ukraine.Presentation.Swagger.Extenstion;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseUkraineSwagger(
		this WebApplication application,
		Action<UkraineSwaggerWebOptions>? options = null)
	{
		var opt = new UkraineSwaggerWebOptions();
		options?.Invoke(opt);

		application.UseSwagger();
		return application.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint(opt.EndpointUrl, opt.EndpointName);

			if (!string.IsNullOrEmpty(opt.OAuthClientId))
				c.OAuthClientId(opt.OAuthClientId);

			if (!string.IsNullOrEmpty(opt.OAuthAppName))
				c.OAuthAppName(opt.OAuthAppName);
		});
	}
}