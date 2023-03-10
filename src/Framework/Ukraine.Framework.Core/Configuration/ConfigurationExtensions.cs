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
}