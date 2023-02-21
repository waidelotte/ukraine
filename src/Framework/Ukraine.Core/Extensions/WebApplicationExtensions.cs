using Microsoft.AspNetCore.Builder;

namespace Ukraine.Core.Extensions;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseUkraineControllers(this WebApplication application)
	{
		application.MapControllers();

		return application;
	}
}