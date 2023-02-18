namespace Ukraine.Services.Identity.Options;

public class IdentityClientOptions
{
	public string? ClientId { get; set; }

	public string? ClientName { get; set; }

	public ICollection<string> AllowedGrantTypes { get; set; } = new HashSet<string>();

	public bool AllowAccessTokensViaBrowser { get; set; }

	public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();

	public ICollection<string> PostLogoutRedirectUris { get; set; } = new HashSet<string>();

	public ICollection<string> AllowedScopes { get; set; } = new HashSet<string>();
}