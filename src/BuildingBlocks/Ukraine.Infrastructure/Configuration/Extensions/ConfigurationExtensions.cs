using Microsoft.Extensions.Configuration;
using Ukraine.Domain.Exceptions;

namespace Ukraine.Infrastructure.Configuration.Extensions;

public static class ConfigurationExtensions
{
	public static TOption GetOption<TOption>(this IConfiguration configuration, string sectionName) where TOption : class
	{
		var options = configuration.GetSection(sectionName).Get<TOption>();
		
		if (options == null) 
			throw CoreException.Exception($"Unable to initialize section: {sectionName}");

		return options;
	}
	
	public static string GetServiceName(this IConfiguration configuration)
	{
		var serviceName = configuration.GetValue<string?>(Constants.SERVICE_NAME_KEY);
		
		if (string.IsNullOrEmpty(serviceName)) 
			throw CoreException.Exception($"Couldn't find service name key [{Constants.SERVICE_NAME_KEY}] in configuration");

		return serviceName;
	}
}