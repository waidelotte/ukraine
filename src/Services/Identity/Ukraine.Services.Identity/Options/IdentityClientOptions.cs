using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Options;

public class IdentityClientOptions
{
	[Required]
	public required string ClientId { get; set; }

	[Required]
	public required string ClientName { get; set; }

	[Required]
	public required ICollection<string> AllowedGrantTypes { get; set; } = new HashSet<string>();

	public bool AllowAccessTokensViaBrowser { get; set; }

	[Required]
	public required ICollection<string> RedirectUris { get; set; } = new HashSet<string>();

	[Required]
	public required ICollection<string> PostLogoutRedirectUris { get; set; } = new HashSet<string>();

	[Required]
	public required ICollection<string> AllowedScopes { get; set; } = new HashSet<string>();
}