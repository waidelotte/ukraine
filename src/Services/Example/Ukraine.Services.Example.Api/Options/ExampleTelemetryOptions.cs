namespace Ukraine.Services.Example.Api.Options;

public class ExampleTelemetryOptions
{
	public const string SECTION_NAME = "TelemetryOptions";

	public string? ZipkinServerUrl { get; set; }

	public bool RecordSqlException { get; set; }
}