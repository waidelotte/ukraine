using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Persistence.Configuration;

public class IdentityRole
{
	[Required]
	public required string Name { get; set; }

	public IReadOnlyCollection<IdentityClaim> Claims { get; set; } = Array.Empty<IdentityClaim>();
}