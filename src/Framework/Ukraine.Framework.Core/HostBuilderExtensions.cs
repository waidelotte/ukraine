using Microsoft.Extensions.Hosting;

namespace Ukraine.Framework.Core;

public static class HostBuilderExtensions
{
	public static IHostBuilder AddServicesValidationOnBuild(this IHostBuilder hostBuilder)
	{
		hostBuilder.UseDefaultServiceProvider((_, options) =>
		{
			options.ValidateScopes = true;
			options.ValidateOnBuild = true;
		});

		return hostBuilder;
	}
}