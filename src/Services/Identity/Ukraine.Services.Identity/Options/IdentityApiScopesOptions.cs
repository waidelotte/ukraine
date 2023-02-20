using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Options;

public class IdentityApiScopesOptions
{
	[Required]
	public required string Name { get; set; }

	[Required]
	public required string DisplayName { get; set; }
}