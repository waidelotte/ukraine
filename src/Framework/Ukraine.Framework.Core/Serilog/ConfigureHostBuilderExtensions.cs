using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Ukraine.Framework.Core.Serilog;

public static class ConfigureHostBuilderExtensions
{
	public static IHostBuilder UseSerilog(
		this IHostBuilder builder,
		IConfiguration configurationSection)
	{
		var loggerConfiguration = new LoggerConfiguration()
			.ReadFrom.Configuration(configurationSection);

		Log.Logger = loggerConfiguration.CreateLogger();

		builder.UseSerilog();

		return builder;
	}
}