using Ukraine.Domain.Abstractions;

namespace Ukraine.Services.Example.Domain.Events;

public class ExampleEmptyEvent : IDomainEvent
{
	public DateTime CreatedAt => DateTime.UtcNow;
}