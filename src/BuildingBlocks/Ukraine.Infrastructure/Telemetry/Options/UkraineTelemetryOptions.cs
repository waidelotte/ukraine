namespace Ukraine.Infrastructure.Telemetry.Options;

public class UkraineTelemetryOptions
{
	public string? ZipkinServerUrl { get; set; }

	public bool RecordSqlException { get; set; }
}