namespace Ukraine.Services.Example.Api.Options;

public class ExampleIdentityOptions
{
	public const string SECTION_NAME = "IdentityOptions";

	public string? Authority { get; set; }

	public string? SwaggerClientId { get; set; }

	public bool RequireHttps { get; set; }

	public bool ValidateAudience { get; set; }

	public IEnumerable<ExampleIdentityScopeOptions> Scopes { get; set; } = Array.Empty<ExampleIdentityScopeOptions>();
}