namespace Ukraine.Domain.Models;

public abstract class EntityBase<TIdentity>
{
	public TIdentity Id { get; set; } = default!;
}