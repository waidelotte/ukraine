using Ukraine.Domain.Interfaces;

namespace Ukraine.Domain.Events;

public abstract record IntegrationEvent(Guid Id, DateTime CreatedAt) : IIntegrationEvent
{
	protected IntegrationEvent() : this(Guid.NewGuid(), DateTime.UtcNow) { }
}
