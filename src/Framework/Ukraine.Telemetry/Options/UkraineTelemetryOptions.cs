using System.ComponentModel.DataAnnotations;

namespace Ukraine.Telemetry.Options;

public class UkraineTelemetryOptions
{
	[Required]
	public required string ServiceName { get; set; }

	public UkraineInstrumentationOptions Instrumentation { get; set; } = new();

	public UkraineExporterOptions Exporter { get; set; } = new();
}