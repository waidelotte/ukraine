namespace Ukraine.Framework.Abstractions;

public abstract record BaseEvent : IEvent<Guid>
{
	public Guid EventId { get; init; } = Guid.NewGuid();
}