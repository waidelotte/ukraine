using Ukraine.Domain.Abstractions;

namespace Ukraine.Domain.Events;

public record IntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}
