using Microsoft.Extensions.Hosting;

namespace Ukraine.Framework.Core;

public static class HostEnvironmentExtensions
{
	public static bool IsDevelopmentDocker(this IHostEnvironment hostEnvironment, bool includeDefault = true)
	{
		return hostEnvironment.IsEnvironment("Development-Docker") || (includeDefault && hostEnvironment.IsDevelopment());
	}
}