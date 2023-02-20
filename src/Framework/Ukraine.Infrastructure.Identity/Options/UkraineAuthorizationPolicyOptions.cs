using System.ComponentModel.DataAnnotations;

namespace Ukraine.Infrastructure.Identity.Options;

public class UkraineAuthorizationPolicyOptions
{
	[Required]
	public required string Name { get; set; }

	public bool RequireAuthenticatedUser { get; set; } = true;

	[Required]
	public required IEnumerable<string> Scopes { get; set; }
}