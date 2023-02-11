namespace Ukraine.Infrastructure.EfCore.Options;

public class UkrainePostgresOptions
{
	public int? RetryOnFailureCount { get; set; }

	public TimeSpan? RetryOnFailureDelay { get; set; }

	public bool UseInMemoryDatabase { get; set; }
}