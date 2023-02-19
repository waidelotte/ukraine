using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Ukraine.Infrastructure.Logging.Options;

namespace Ukraine.Infrastructure.Logging.Extenstion;

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

		var loggingOptions = configurationSection.Get<UkraineLoggingOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (loggingOptions == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		var loggerConfiguration = new LoggerConfiguration();

		loggerConfiguration.MinimumLevel.Is(loggingOptions.MinimumLevel);

		foreach (var minLevelOverride in loggingOptions.Override)
		{
			loggerConfiguration.MinimumLevel.Override(minLevelOverride.Key, minLevelOverride.Value);
		}

		if (loggingOptions.WriteTo.Console)
			loggerConfiguration.WriteTo.Console(theme: AnsiConsoleTheme.Code);

		if (!string.IsNullOrEmpty(loggingOptions.WriteTo.SeqServerUrl))
			loggerConfiguration.WriteTo.Seq(loggingOptions.WriteTo.SeqServerUrl);

		loggerConfiguration.Enrich.WithProperty("ServiceName", loggingOptions.ServiceName);

		loggerConfiguration
			.Enrich.FromLogContext()
			.Enrich.WithMachineName()
			.Enrich.WithEnvironmentName();

		Log.Logger = loggerConfiguration.CreateLogger();

		builder.UseSerilog();

		return builder;
	}
}