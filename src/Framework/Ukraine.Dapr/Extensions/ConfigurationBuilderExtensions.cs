using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.Extensions.Configuration;

namespace Ukraine.Dapr.Extensions;

public static class ConfigurationBuilderExtensions
{
	public static IConfigurationBuilder AddUkraineDaprSecretStore(this IConfigurationBuilder configurationManager, string store)
	{
		return configurationManager.AddDaprSecretStore(store, new DaprClientBuilder().Build());
	}
}