using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Example.Api.Swagger.Options;

internal sealed class ServiceSwaggerSecurityOptions
{
	[Required]
	public required string ClientId { get; set; }

	[Required]
	public required string ClientName { get; set; }

	[Required]
	public required Uri AuthorizationUrl { get; set; }

	[Required]
	public required Uri TokenUrl { get; set; }

	[Required]
	public required Dictionary<string, string> Scopes { get; set; }
}