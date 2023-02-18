namespace Ukraine.Services.Identity.Options;

public class IdentityUserOptions
{
	public IdentityUserEmailOptions Email { get; set; } = new();

	public IdentityUserPasswordOptions Password { get; set; } = new();
}