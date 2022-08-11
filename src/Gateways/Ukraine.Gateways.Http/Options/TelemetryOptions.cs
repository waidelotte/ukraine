namespace Ukraine.Gateways.Http.Options;

public class TelemetryOptions
{
    public const string Position = "Telemetry";
    
    public bool UseZipkin { get; set; } = false;
    public string ZipkinEndpoint { get; set; } = string.Empty;
}