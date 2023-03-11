using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;

namespace Ukraine.Framework.Core.Configuration;

public static class ConfigurationExtensions
{
	public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
	{
		var connectionString = configuration.GetConnectionString(name);

		Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));

		return connectionString;
	}

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