using Microsoft.AspNetCore.Builder;

namespace Ukraine.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseUkraineControllers(this WebApplication application)
	{
		application.MapControllers();

		return application;
	}
}