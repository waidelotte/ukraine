namespace Ukraine.Services.Identity.Options;

internal sealed class IdentityUserOptions
{
	public IdentityUserEmailOptions Email { get; set; } = new();

	public IdentityUserPasswordOptions Password { get; set; } = new();
}