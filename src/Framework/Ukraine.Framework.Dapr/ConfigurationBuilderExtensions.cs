using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.Extensions.Configuration;

namespace Ukraine.Framework.Dapr;

public static class ConfigurationBuilderExtensions
{
	public static IConfigurationBuilder AddDaprSecretStore(this IConfigurationBuilder configurationManager, string store)
	{
		return configurationManager.AddDaprSecretStore(store, new DaprClientBuilder().Build());
	}
}