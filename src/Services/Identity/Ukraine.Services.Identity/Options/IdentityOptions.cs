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

	public ICollection<IdentityApiScopesOptions> ApiScopes { get; set; } = new HashSet<IdentityApiScopesOptions>();

	public ICollection<IdentityClientOptions> Clients { get; set; } = new HashSet<IdentityClientOptions>();
}