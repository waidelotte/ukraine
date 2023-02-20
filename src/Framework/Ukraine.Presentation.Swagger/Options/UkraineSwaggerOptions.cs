using System.ComponentModel.DataAnnotations;

namespace Ukraine.Presentation.Swagger.Options;

public class UkraineSwaggerOptions
{
	[Required]
	public required string ServiceTitle { get; set; }

	[Required]
	public required string Version { get; set; } = Constants.DEFAULT_VERSION;

	[Required]
	public required string Endpoint { get; set; } = Constants.DEFAULT_ENDPOINT;

	[Required]
	public required string EndpointName { get; set; } = Constants.DEFAULT_ENDPOINT_NAME;

	public UkraineOAuth2SecurityOptions? OAuth2Security { get; set; }
}