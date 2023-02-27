using Microsoft.Extensions.Configuration;

namespace Ukraine.Framework.Core.Configuration;

public static class ConfigurationExtensions
{
	public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
	{
		var connectionString = configuration.GetConnectionString(name);

		if (string.IsNullOrEmpty(connectionString))
			throw new KeyNotFoundException("Connection string is null or empty");

		return connectionString;
	}
}