namespace Ukraine.Infrastructure.EfCore.Options;

public class UkrainePostgresOptions
{
	public int? RetryOnFailureCount { get; set; }

	public TimeSpan? RetryOnFailureDelay { get; set; }

	public bool SensitiveDataLogging { get; set; }

	public bool DetailedErrors { get; set; }
}