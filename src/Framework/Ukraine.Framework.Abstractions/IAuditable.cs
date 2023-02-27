namespace Ukraine.Framework.Abstractions;

public interface IAuditable
{
	public DateTime CreatedUtc { get; set; }

	public DateTime? LastModifiedUtc { get; set; }
}