using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.Core;

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

	public static IServiceCollection AddSystemDateTimeProvider(this IServiceCollection services)
	{
		services.TryAddTransient<IDateTimeProvider, SystemDateTimeProvider>();
		return services;
	}
}