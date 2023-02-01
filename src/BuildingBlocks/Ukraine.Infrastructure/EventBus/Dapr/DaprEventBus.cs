using Dapr.Client;
using Microsoft.Extensions.Logging;
using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EventBus.Dapr;

public class DaprEventBus : IEventBus
{
    private readonly DaprClient _dapr;
    private readonly ILogger _logger;

    public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
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
