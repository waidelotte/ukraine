namespace Ukraine.Services.Identity.Options;

public class IdentityUserPasswordOptions
{
	public bool RequireDigit { get; set; } = true;

	public int RequiredLength { get; set; } = 6;

	public bool RequireUppercase { get; set; } = true;

	public bool RequireLowercase { get; set; } = true;

	public bool RequireNonAlphanumeric { get; set; } = true;
}