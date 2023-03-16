namespace Ukraine.Framework.Abstractions;

public abstract class EntityBase : IEntity<Guid>
{
	public Guid Id { get; protected set; } = Guid.NewGuid();

	public DateTime Created { get; protected set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

	public DateTime? Updated { get; protected set; }
}