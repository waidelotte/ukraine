using Microsoft.Extensions.Hosting;

namespace Ukraine.Infrastructure.Extensions;

public static class HostBuilderExtensions
{
	public static IHostBuilder AddUkraineServicesValidationOnBuild(this IHostBuilder hostBuilder)
	{
		hostBuilder.UseDefaultServiceProvider((_, options) =>
		{
			options.ValidateScopes = true;
			options.ValidateOnBuild = true;
		});

		return hostBuilder;
	}
}