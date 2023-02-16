using Microsoft.AspNetCore.Builder;

namespace Ukraine.Presentation.Swagger.Extenstion;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseUkraineSwagger(this WebApplication application)
	{
		application.UseSwagger();
		return application.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint(Constants.ENDPOINT, Constants.DEFAULT_ENDPOINT_NAME);
		});
	}
}