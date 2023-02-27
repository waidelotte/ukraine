using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Example.Api.Swagger.Options;

internal sealed class ServiceSwaggerOptions
{
	public const string CONFIGURATION_SECTION = "Swagger";

	[Required]
	public required string ServiceTitle { get; set; }

	[Required]
	public required string Version { get; set; }

	[Required]
	public required ServiceSwaggerSecurityOptions Security { get; set; }
}