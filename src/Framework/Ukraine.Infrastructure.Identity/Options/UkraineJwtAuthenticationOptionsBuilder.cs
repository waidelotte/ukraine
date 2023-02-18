namespace Ukraine.Infrastructure.Identity.Options;

public class UkraineJwtAuthenticationOptionsBuilder
{
	public string? Authority { get; set; }

	public string? Audience { get; set; }

	public bool RequireHttps { get; set; }
}