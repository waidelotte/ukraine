namespace Ukraine.EfCore.Options;

public class UkrainePostgresOptions : UkraineDatabaseOptions
{
	public int? RetryOnFailureCount { get; set; }

	public TimeSpan? RetryOnFailureDelay { get; set; }

	public void SetOptions(UkrainePostgresOptions options)
	{
		base.SetOptions(options);
		RetryOnFailureCount = options.RetryOnFailureCount;
		RetryOnFailureCount = options.RetryOnFailureCount;
	}
}