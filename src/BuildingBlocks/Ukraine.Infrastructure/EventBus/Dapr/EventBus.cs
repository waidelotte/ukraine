using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Ukraine.Domain.Interfaces;

namespace Ukraine.Infrastructure.EventBus.Dapr;

internal sealed class EventBus : IEventBus
{
    private readonly DaprClient _dapr;
    private readonly ILogger _logger;

    public EventBus(DaprClient dapr, ILogger<EventBus> logger)
    {
        _dapr = dapr;
        _logger = logger;
    }

    public async Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        var topicName = integrationEvent.GetType().Name;

        _logger.LogInformation(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            integrationEvent, Constants.PUB_SUB_NAME, topicName);
        
        await _dapr.PublishEventAsync(Constants.PUB_SUB_NAME, topicName, (object)integrationEvent, cancellationToken);
    }
}
