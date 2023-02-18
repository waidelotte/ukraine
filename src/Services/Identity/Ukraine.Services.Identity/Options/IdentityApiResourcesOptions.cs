namespace Ukraine.Services.Identity.Options;

public class IdentityApiResourcesOptions
{
	public string? Name { get; set; }

	public string? DisplayName { get; set; }

	public IEnumerable<string> Scopes { get; set; } = Array.Empty<string>();
}