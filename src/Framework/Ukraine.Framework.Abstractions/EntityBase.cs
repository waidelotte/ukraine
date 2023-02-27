namespace Ukraine.Framework.Abstractions;

public abstract class EntityBase<TIdentity>
	where TIdentity : struct
{
	public TIdentity Id { get; protected init; }
}