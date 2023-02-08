using Microsoft.Extensions.Configuration;
using Ukraine.Domain.Exceptions;

namespace Ukraine.Infrastructure.Configuration.Extensions;

public static class ConfigurationExtensions
{
	public static TOption GetRequiredOption<TOption>(this IConfiguration configuration, string sectionName) where TOption : class
	{
		var options = configuration.GetSection(sectionName).Get<TOption>();
		
		if (options == null) 
			throw CoreException.Exception($"Unable to initialize section: {sectionName}");

		return options;
	}
	
	public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
	{
		var value = configuration.GetValue<T>(key);
		
		if (value == null) 
			throw CoreException.Exception($"The Key [{Constants.SERVICE_NAME_KEY}] value is null in configuration");

		return value;
	}
	
	public static string GetRequiredServiceName(this IConfiguration configuration)
	{
		return GetRequiredValue<string>(configuration, Constants.SERVICE_NAME_KEY);
	}
}