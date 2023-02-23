using Microsoft.Extensions.Configuration;

namespace Ukraine.Core.Extensions;

public static class ConfigurationExtensions
{
	public static TOptions GetOptions<TOptions>(
		this IConfigurationSection configurationSection,
		bool errorOnUnknownConfiguration = true)
		where TOptions : class
	{
		var options = configurationSection.Get<TOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = errorOnUnknownConfiguration;
		});

		if (options == null)
		{
			throw new ArgumentNullException(
				nameof(configurationSection),
				$"Configuration Section [{configurationSection.Key}] is empty");
		}

		return options;
	}
}