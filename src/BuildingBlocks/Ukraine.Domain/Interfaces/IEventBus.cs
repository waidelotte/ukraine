namespace Ukraine.Domain.Interfaces;

public interface IEventBus
{
    Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}
