using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Options;

public class IdentityOptions
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

	[Required]
	public required IEnumerable<IdentityApiResourcesOptions> ApiResources { get; set; } = Array.Empty<IdentityApiResourcesOptions>();

	[Required]
	public required IEnumerable<IdentityApiScopesOptions> ApiScopes { get; set; } = Array.Empty<IdentityApiScopesOptions>();

	[Required]
	public required IEnumerable<IdentityClientOptions> Clients { get; set; } = Array.Empty<IdentityClientOptions>();
}