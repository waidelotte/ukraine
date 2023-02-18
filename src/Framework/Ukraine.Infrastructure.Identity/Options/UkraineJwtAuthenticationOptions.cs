namespace Ukraine.Infrastructure.Identity.Options;

public class UkraineJwtAuthenticationOptions
{
	public string? Authority { get; set; }

	public bool RequireHttps { get; set; } = true;

	public bool ValidateAudience { get; set; } = true;
}