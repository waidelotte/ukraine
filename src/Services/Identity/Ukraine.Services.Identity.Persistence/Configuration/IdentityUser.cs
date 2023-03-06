using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Persistence.Configuration;

public class IdentityUser
{
	[Required]
	public required string Username { get; set; }

	[Required]
	public required string Email { get; set; }

	[Required]
	public required string Password { get; set; }

	public IReadOnlyCollection<IdentityClaim> Claims { get; set; } = Array.Empty<IdentityClaim>();

	public IReadOnlyCollection<string> Roles { get; set; } = Array.Empty<string>();
}