namespace Ukraine.Services.Identity.Options;

public class IdentityDatabaseOptions
{
	public const string SECTION_NAME = "DatabaseOptions";

	public int? RetryOnFailureCount { get; set; }

	public TimeSpan? RetryOnFailureDelay { get; set; }

	public bool SensitiveDataLogging { get; set; }

	public bool DetailedErrors { get; set; }
}