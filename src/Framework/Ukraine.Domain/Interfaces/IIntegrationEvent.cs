namespace Ukraine.Domain.Interfaces;

public interface IIntegrationEvent
{
	Guid EventId { get; }

	DateTime CreatedAt { get; }
}