using Microsoft.Extensions.Hosting;

namespace Ukraine.Infrastructure.Hosting.Extensions;

public static class HostBuilderExtensions
{
	public static IHostBuilder UseUkraineServicesValidationOnBuild(this IHostBuilder hostBuilder)
	{
		hostBuilder.UseDefaultServiceProvider((_, options) =>
		{
			options.ValidateScopes = true;
			options.ValidateOnBuild = true;
		});

		return hostBuilder;
	}
}