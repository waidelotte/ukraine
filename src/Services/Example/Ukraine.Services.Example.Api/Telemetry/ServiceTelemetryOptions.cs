using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Example.Api.Telemetry;

internal sealed class ServiceTelemetryOptions
{
	public const string CONFIGURATION_SECTION = "Telemetry";

	[Required]
	public required Uri ZipkinServerUrl { get; set; }
}