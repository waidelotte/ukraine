namespace Ukraine.Services.Example.Api.Options;

public class ExampleDatabaseOptions
{
	public int? RetryOnFailureCount { get; set; }
	public TimeSpan? RetryOnFailureDelay { get; set; }
}