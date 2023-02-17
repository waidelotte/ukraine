namespace Ukraine.Presentation.Swagger.Options;

public class UkraineSwaggerWebOptions
{
	public string? EndpointUrl { get; set; } = Constants.ENDPOINT;

	public string? EndpointName { get; set; } = Constants.DEFAULT_ENDPOINT_NAME;

	public string? OAuthClientId { get; set; }

	public string? OAuthAppName { get; set; }
}