namespace Ukraine.Persistence.EfCore.Options;

public class UkrainePostgresOptions : UkraineDatabaseOptions
{
	public int? RetryOnFailureCount { get; set; }

	public TimeSpan? RetryOnFailureDelay { get; set; }
}