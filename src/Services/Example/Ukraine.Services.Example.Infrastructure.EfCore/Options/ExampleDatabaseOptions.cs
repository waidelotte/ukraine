namespace Ukraine.Services.Example.Infrastructure.EfCore.Options;

public class ExampleDatabaseOptions
{
	public const string SectionName = "CustomDatabase";
	
	public int RetryOnFailureCount { get; set; }
	public TimeSpan RetryOnFailureDelay { get; set; }
}