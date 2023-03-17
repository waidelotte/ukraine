namespace Ukraine.Framework.Abstractions;

public abstract class AuditableEntityBase : EntityBase, IAuditableEntity<Guid>
{
	public DateTime CreatedUtc { get; set; }

	public DateTime? LastModifiedUtc { get; set; }
}