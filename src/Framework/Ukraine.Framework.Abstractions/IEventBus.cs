namespace Ukraine.Framework.Abstractions;

public interface IEventBus
{
	Task PublishAsync(IEvent eventData, CancellationToken cancellationToken = default);
}