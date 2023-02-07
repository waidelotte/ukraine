namespace Ukraine.Domain.Interfaces;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
}