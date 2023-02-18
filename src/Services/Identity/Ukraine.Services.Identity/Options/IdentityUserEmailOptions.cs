namespace Ukraine.Services.Identity.Options;

public class IdentityUserEmailOptions
{
	public bool RequireConfirmed { get; set; } = true;

	public bool RequireUnique { get; set; } = true;
}