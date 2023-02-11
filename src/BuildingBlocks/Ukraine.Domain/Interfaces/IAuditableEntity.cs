namespace Ukraine.Domain.Interfaces;

public interface IAuditableEntity
{
	public DateTime CreatedUtc { get; set; }

	public DateTime? LastModifiedUtc { get; set; }
}