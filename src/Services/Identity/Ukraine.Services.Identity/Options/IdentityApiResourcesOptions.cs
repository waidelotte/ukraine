using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Options;

public class IdentityApiResourcesOptions
{
	[Required]
	public required string Name { get; set; }

	[Required]
	public required string DisplayName { get; set; }

	[Required]
	public required IEnumerable<string> Scopes { get; set; } = Array.Empty<string>();
}