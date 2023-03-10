using Ardalis.GuardClauses;
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

		Guard.Against.Null(options, message: $"Configuration Section [{configurationSection.Key}] is empty");

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