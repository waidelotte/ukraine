using Microsoft.Extensions.Configuration;

namespace Ukraine.Framework.Core.Options;

public static class ConfigurationExtensions
{
	public static TOptions GetOptions<TOptions>(
		this IConfigurationSection configurationSection,
		bool errorOnUnknownConfiguration = true)
		where TOptions : class
	{
		var options = Options<TOptions>(configurationSection, errorOnUnknownConfiguration);

		if (options == null)
		{
			throw new KeyNotFoundException(
				$"Configuration Section [{configurationSection.Key}] is empty");
		}

		return options;
	}

	public static TOptions GetOptions<TOptions>(
		this IConfiguration configuration,
		bool errorOnUnknownConfiguration = true)
		where TOptions : class
	{
		var options = Options<TOptions>(configuration, errorOnUnknownConfiguration);

		if (options == null)
			throw new KeyNotFoundException("Configuration Section is empty");

		return options;
	}

	private static TOptions? Options<TOptions>(
		this IConfiguration configuration,
		bool errorOnUnknownConfiguration = true)
		where TOptions : class
	{
		var options = configuration.Get<TOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = errorOnUnknownConfiguration;
		});

		return options;
	}
}