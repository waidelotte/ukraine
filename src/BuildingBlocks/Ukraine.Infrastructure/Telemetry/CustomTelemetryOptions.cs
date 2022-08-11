namespace Ukraine.Infrastructure.Telemetry;

public class CustomTelemetryOptions
{
    public string? ApplicationName { get; set; }
    public bool UseZipkin { get; set; } = false;
    public string ZipkinEndpoint { get; set; } = string.Empty;
}