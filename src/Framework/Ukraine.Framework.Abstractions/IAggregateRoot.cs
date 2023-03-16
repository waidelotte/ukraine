namespace Ukraine.Framework.Abstractions;

public interface IAggregateRoot : IEntity
{
	public HashSet<IEvent>? DomainEvents { get; }
}