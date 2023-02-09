using Microsoft.Extensions.Configuration;
using Ukraine.Domain.Exceptions;

namespace Ukraine.Infrastructure.Configuration.Extensions;

public static class ConfigurationExtensions
{
	public static T GetRequiredSection<T>(this IConfiguration configuration, string sectionName)
	{
		var value = configuration.GetRequiredSection(sectionName).Get<T>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});
		
		if (value == null) 
			throw CoreException.Exception($"Cannot bind the Configuration instance to a new instance of type {typeof(T)}");
	
		return value;
	}
}