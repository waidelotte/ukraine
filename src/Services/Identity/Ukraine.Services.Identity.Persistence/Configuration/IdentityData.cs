namespace Ukraine.Services.Identity.Persistence.Configuration;

public class IdentityData
{
	public IReadOnlyCollection<IdentityRole> Roles { get; set; } = Array.Empty<IdentityRole>();

	public IReadOnlyCollection<IdentityUser> Users { get; set; } = Array.Empty<IdentityUser>();
}