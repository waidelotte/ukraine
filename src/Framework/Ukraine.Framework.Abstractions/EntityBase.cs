namespace Ukraine.Framework.Abstractions;

public abstract class EntityBase : IEntity<Guid>
{
	public Guid Id { get; protected set; } = Guid.NewGuid();
}