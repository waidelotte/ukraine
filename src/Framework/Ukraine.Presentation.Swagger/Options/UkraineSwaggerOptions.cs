namespace Ukraine.Presentation.Swagger.Options;

public class UkraineSwaggerOptions
{
	public string? Title { get; set; } = Constants.DEFAULT_TITLE;

	public string? Version { get; set; } = Constants.DEFAULT_VERSION;

	public string? IdentityServerUrl { get; set; }

	public Dictionary<string, string> AuthScopes { get; set; } = new();
}