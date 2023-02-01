namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleHealthCheckOptions
{
	public const string SectionName = "CustomHealthChecks";
	
	public bool IsEnabled { get; set; }
	public bool CheckDatabase { get; set; }
	public bool CheckDaprSidecar { get; set; }
}