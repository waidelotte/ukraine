using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Models;

namespace Ukraine.Presentation.Swagger.Options;

public class UkraineOAuth2SecurityOptions
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
	public required Dictionary<string, string> Scopes { get; set; } = new();
}