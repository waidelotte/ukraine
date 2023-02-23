using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ukraine.Core.Extensions;

public static class ServiceCollectionExtensions
{
	public static TOptions BindAndGetOptions<TOptions>(
		this IServiceCollection serviceCollection,
		IConfigurationSection configurationSection,
		bool errorOnUnknownConfiguration = true)
		where TOptions : class
	{
		BindOptions<TOptions>(serviceCollection, configurationSection);

		return configurationSection.GetOptions<TOptions>(errorOnUnknownConfiguration);
	}

	public static OptionsBuilder<TOptions> BindOptions<TOptions>(
		this IServiceCollection serviceCollection,
		IConfigurationSection configurationSection)
		where TOptions : class
	{
		return serviceCollection.AddOptions<TOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();
	}
}