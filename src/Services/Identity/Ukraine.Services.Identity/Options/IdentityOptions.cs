using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Options;

internal sealed class IdentityOptions
{
	public const string SECTION_NAME = "IdentityOptions";

	[Required]
	public required string IssuerUri { get; set; }

	public TimeSpan CookieLifetime { get; set; } = TimeSpan.FromHours(2);

	public bool EmitStaticAudienceClaim { get; set; }

	public bool RaiseErrorEvents { get; set; }

	public bool RaiseInformationEvents { get; set; }

	public bool RaiseFailureEvents { get; set; }

	public bool RaiseSuccessEvents { get; set; }

	[Required]
	public required IdentityUserOptions User { get; set; } = new();
}