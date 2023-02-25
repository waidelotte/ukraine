using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Ukraine.Core.Logging.Options;

namespace Ukraine.Core.Logging.Extenstion;

public static class ConfigureHostBuilderExtensions
{
	public static ConfigureHostBuilder AddUkraineSerilog(
		this ConfigureHostBuilder builder,
		IServiceCollection serviceCollection,
		IConfigurationSection configurationSection)
	{
		serviceCollection.AddOptions<UkraineLoggingOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var options = configurationSection.Get<UkraineLoggingOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (options == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		var loggerConfiguration = new LoggerConfiguration();

		loggerConfiguration.MinimumLevel.Is(options.MinimumLevel);

		foreach (var minLevelOverride in options.Override)
		{
			loggerConfiguration.MinimumLevel.Override(minLevelOverride.Key, minLevelOverride.Value);
		}

		if (options.WriteTo.Console)
			loggerConfiguration.WriteTo.Console(theme: AnsiConsoleTheme.Code);

		if (!string.IsNullOrEmpty(options.WriteTo.SeqServerUrl))
			loggerConfiguration.WriteTo.Seq(options.WriteTo.SeqServerUrl);

		loggerConfiguration.Enrich.WithProperty("ServiceName", options.ServiceName);

		loggerConfiguration
			.Enrich.FromLogContext()
			.Enrich.WithMachineName()
			.Enrich.WithEnvironmentName();

		Log.Logger = loggerConfiguration.CreateLogger();

		builder.UseSerilog();

		return builder;
	}
}