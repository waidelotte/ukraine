using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.Dapr;

internal sealed class DaprEventBus : IEventBus
{
	private readonly IOptions<DaprEventBusOptions> _options;
	private readonly DaprClient _daprClient;
	private readonly ILogger _logger;

	public DaprEventBus(IOptions<DaprEventBusOptions> options, DaprClient daprClient, ILogger<DaprEventBus> logger)
	{
		_options = options;
		_daprClient = daprClient;
		_logger = logger;
	}

	public async Task PublishAsync(IEvent eventData, CancellationToken cancellationToken = default)
	{
		var topicName = eventData.GetType().Name;
		var pubsubName = _options.Value.PubSubName;

		_logger.LogInformation(
			"Publishing event {@Event} to {PubsubName}.{TopicName}",
			eventData,
			pubsubName,
			topicName);

		await _daprClient.PublishEventAsync(pubsubName, topicName, (object)eventData, cancellationToken);
	}
}