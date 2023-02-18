using Microsoft.AspNetCore.Builder;
using Ukraine.Presentation.Swagger.Options;

namespace Ukraine.Presentation.Swagger.Extenstion;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseUkraineSwagger(
		this WebApplication application,
		Action<UkraineSwaggerWebOptionsBuilder>? configure = null)
	{
		var builder = new UkraineSwaggerWebOptionsBuilder();
		configure?.Invoke(builder);

		application.UseSwagger();
		return application.UseSwaggerUI(builder.Build());
	}
}