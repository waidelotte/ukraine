namespace Ukraine.Framework.Abstractions;

public abstract class AuditableBase<TIdentity> : EntityRootBase<TIdentity>, IAuditable
	where TIdentity : struct
{
	public DateTime CreatedUtc { get; set; }

	public DateTime? LastModifiedUtc { get; set; }
}