using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ukraine.Domain.Interfaces;
using Ukraine.EventBus.Options;

namespace Ukraine.EventBus;

internal sealed class DaprEventBus : IEventBus
{
	private readonly IOptionsSnapshot<UkraineDaprOptions> _options;
	private readonly DaprClient _dapr;
	private readonly ILogger _logger;

	public DaprEventBus(IOptionsSnapshot<UkraineDaprOptions> optionsMonitor, DaprClient dapr, ILogger<DaprEventBus> logger)
	{
		_options = optionsMonitor;
		_dapr = dapr;
		_logger = logger;
	}

	public async Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
	{
		var topicName = integrationEvent.GetType().Name;
		var pubsubName = _options.Value.PubSubName;

		_logger.LogInformation(
			"Publishing event {@Event} to {PubsubName}.{TopicName}",
			integrationEvent,
			pubsubName,
			topicName);

		await _dapr.PublishEventAsync(pubsubName, topicName, (object)integrationEvent, cancellationToken);
	}
}