namespace Ukraine.Services.Identity.Options;

public class IdentityOptions
{
	public const string SECTION_NAME = "IdentityOptions";

	public string? IssuerUri { get; set; }

	public TimeSpan CookieLifetime { get; set; } = TimeSpan.FromHours(2);

	public bool EmitStaticAudienceClaim { get; set; }

	public bool RaiseErrorEvents { get; set; }

	public bool RaiseInformationEvents { get; set; }

	public bool RaiseFailureEvents { get; set; }

	public bool RaiseSuccessEvents { get; set; }

	public IdentityUserOptions User { get; set; } = new();

	public IEnumerable<IdentityApiResourcesOptions> ApiResources { get; set; } = Array.Empty<IdentityApiResourcesOptions>();

	public IEnumerable<IdentityApiScopesOptions> ApiScopes { get; set; } = Array.Empty<IdentityApiScopesOptions>();

	public IEnumerable<IdentityClientOptions> Clients { get; set; } = Array.Empty<IdentityClientOptions>();
}