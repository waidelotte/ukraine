namespace Ukraine.EfCore.Options;

public class UkraineDatabaseOptions
{
	public bool EnableDetailedErrors { get; set; }

	public bool EnableSensitiveDataLogging { get; set; }

	public void SetOptions(UkraineDatabaseOptions options)
	{
		EnableDetailedErrors = options.EnableDetailedErrors;
		EnableSensitiveDataLogging = options.EnableSensitiveDataLogging;
	}
}