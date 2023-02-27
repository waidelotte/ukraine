using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Ukraine.Framework.Core.Serilog;

public static class ConfigureHostBuilderExtensions
{
	public static ConfigureHostBuilder UseSerilog(
		this ConfigureHostBuilder builder,
		IConfiguration configurationSection)
	{
		var loggerConfiguration = new LoggerConfiguration()
			.ReadFrom.Configuration(configurationSection);

		Log.Logger = loggerConfiguration.CreateLogger();

		builder.UseSerilog();

		return builder;
	}
}