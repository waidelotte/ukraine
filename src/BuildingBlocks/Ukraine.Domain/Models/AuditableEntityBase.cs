using Ukraine.Domain.Interfaces;

namespace Ukraine.Domain.Models;

public abstract class AuditableEntityBase<TIdentity> : EntityRootBase<TIdentity>, IAuditableEntity
	where TIdentity : struct
{
	public DateTime CreatedUtc { get; set; }

	public DateTime? LastModifiedUtc { get; set; }
}