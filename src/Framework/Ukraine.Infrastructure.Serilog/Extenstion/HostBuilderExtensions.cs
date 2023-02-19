using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Ukraine.Infrastructure.Serilog.Options;

namespace Ukraine.Infrastructure.Serilog.Extenstion;

public static class HostBuilderExtensions
{
	public static IHostBuilder UseUkraineSerilog(
		this IHostBuilder hostBuilder,
		Action<UkraineLoggingOptions>? configure = null)
	{
		var options = new UkraineLoggingOptions();
		configure?.Invoke(options);

		var loggerConfiguration = new LoggerConfiguration()
			.MinimumLevel.Is(options.MinimumLevel);

		foreach (var minLevelOverride in options.OverrideDictionary)
		{
			loggerConfiguration.MinimumLevel.Override(minLevelOverride.Key, minLevelOverride.Value);
		}

		var writeOptions = new UkraineLoggingWriteOptions();
		options.WriteTo?.Invoke(writeOptions);

		loggerConfiguration.WriteTo.Console(theme: AnsiConsoleTheme.Code);

		if (!string.IsNullOrEmpty(writeOptions.WriteToSeqServerUrl))
			loggerConfiguration.WriteTo.Seq(writeOptions.WriteToSeqServerUrl);

		if (!string.IsNullOrEmpty(options.ServiceName))
			loggerConfiguration.Enrich.WithProperty(Constants.ENRICH_SERVICE_PROPERTY, options.ServiceName);

		loggerConfiguration
			.Enrich.FromLogContext()
			.Enrich.WithMachineName()
			.Enrich.WithEnvironmentName();

		Log.Logger = loggerConfiguration.CreateLogger();

		hostBuilder.UseSerilog();

		return hostBuilder;
	}
}