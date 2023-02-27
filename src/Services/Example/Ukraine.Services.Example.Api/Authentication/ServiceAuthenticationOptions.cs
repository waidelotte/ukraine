using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Example.Api.Authentication;

internal sealed class ServiceAuthenticationOptions
{
	public const string CONFIGURATION_SECTION = "Authentication";

	[Required]
	public required string Authority { get; set; }

	public bool RequireHttpsMetadata { get; set; } = true;
}