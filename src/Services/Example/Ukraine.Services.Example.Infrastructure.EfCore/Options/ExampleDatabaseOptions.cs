namespace Ukraine.Services.Example.Infrastructure.EfCore.Options;

public class ExampleDatabaseOptions
{
	public int RetryOnFailureCount { get; set; }
	public TimeSpan RetryOnFailureDelay { get; set; }
}