using Microsoft.AspNetCore.Builder;

namespace Ukraine.Infrastructure.EventBus.Extensions;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseUkraineDaprEventBus(this WebApplication application)
	{
		application.UseCloudEvents();
		application.MapSubscribeHandler();

		return application;
	}
}