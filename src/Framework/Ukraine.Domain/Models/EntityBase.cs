namespace Ukraine.Domain.Models;

public abstract class EntityBase<TIdentity>
	where TIdentity : struct
{
	public TIdentity Id { get; protected init; }
}