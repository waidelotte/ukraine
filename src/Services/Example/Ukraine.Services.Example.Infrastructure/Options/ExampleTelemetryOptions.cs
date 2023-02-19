namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleTelemetryOptions
{
	public const string SECTION_NAME = "Telemetry";

	public string? ZipkinServerUrl { get; set; }

	public bool RecordSqlException { get; set; }
}