namespace Ukraine.Services.Example.Persistence.Options;

internal sealed class ServiceDatabaseOptions
{
	public const string CONFIGURATION_SECTION = "Database";

	public bool EnableDetailedErrors { get; set; }

	public bool EnableSensitiveDataLogging { get; set; }
}