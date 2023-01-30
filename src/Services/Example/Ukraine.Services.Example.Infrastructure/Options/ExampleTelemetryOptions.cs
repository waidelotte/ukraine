namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleTelemetryOptions
{
	public const string SectionName = "CustomTelemetry";
	
	public bool UseZipkin { get; set; }
	public string? ZipkinServerUrl { get; set; }
}