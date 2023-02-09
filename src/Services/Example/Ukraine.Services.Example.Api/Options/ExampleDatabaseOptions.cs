namespace Ukraine.Services.Example.Api.Options;

public class ExampleDatabaseOptions
{
	public const string SECTION_NAME = "DatabaseOptions";
	
	public int? RetryOnFailureCount { get; set; }
	public TimeSpan? RetryOnFailureDelay { get; set; }
}