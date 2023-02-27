using Microsoft.Extensions.Options;
using Ukraine.Services.Example.Api.Swagger.Options;

namespace Ukraine.Services.Example.Api.Swagger.Extenstion;

internal static class WebApplicationExtensions
{
	public static IApplicationBuilder UseServiceSwagger(this WebApplication application)
	{
		var options = application.Services.GetRequiredService<IOptions<ServiceSwaggerOptions>>().Value;

		application.UseSwagger();
		return application.UseSwaggerUI(o =>
		{
			o.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
			o.OAuthClientId(options.Security.ClientId);
			o.OAuthAppName(options.Security.ClientName);
		});
	}
}