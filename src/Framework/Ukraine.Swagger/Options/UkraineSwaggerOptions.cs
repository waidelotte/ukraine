using System.ComponentModel.DataAnnotations;

namespace Ukraine.Swagger.Options;

public class UkraineSwaggerOptions
{
	[Required]
	public required string ServiceTitle { get; set; }

	[Required]
	public required string Version { get; set; } = "v1";

	[Required]
	public required string Endpoint { get; set; } = "/swagger/v1/swagger.json";

	[Required]
	public required string EndpointName { get; set; } = "API";

	public UkraineOAuth2SecurityOptions? OAuth2Security { get; set; }
}