namespace Ukraine.Services.Identity.Options;

internal sealed class IdentityUserEmailOptions
{
	public bool RequireConfirmed { get; set; } = true;

	public bool RequireUnique { get; set; } = true;
}