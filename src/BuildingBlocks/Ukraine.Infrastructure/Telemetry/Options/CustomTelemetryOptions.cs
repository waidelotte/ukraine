namespace Ukraine.Infrastructure.Telemetry.Options;

public class CustomTelemetryOptions
{
    public string? ApplicationName { get; set; }
    public bool UseZipkin { get; set; }
    public string? ZipkinEndpoint { get; set; }
}