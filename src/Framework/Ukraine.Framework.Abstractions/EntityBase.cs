namespace Ukraine.Framework.Abstractions;

public abstract class EntityBase<TIdentity> : IEntity<TIdentity>
	where TIdentity : struct
{
	public TIdentity Id { get; protected set; }

	public DateTime Created { get; protected set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

	public DateTime? Updated { get; protected set; }
}