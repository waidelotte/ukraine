using Ukraine.Domain.Abstractions;

namespace Ukraine.Domain.Events;

public record IntegrationEvent(Guid Id, DateTime CreatedAt) : IIntegrationEvent
{
	public IntegrationEvent() : this(Guid.NewGuid(), DateTime.UtcNow) { }
}
