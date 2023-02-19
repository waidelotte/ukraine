namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleSwaggerOptions
{
	public const string SECTION_NAME = "Swagger";

	public string? OAuthClientId { get; set; }

	public IEnumerable<string> AuthScopes { get; set; } = Array.Empty<string>();
}