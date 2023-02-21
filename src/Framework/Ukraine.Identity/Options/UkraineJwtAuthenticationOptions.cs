using System.ComponentModel.DataAnnotations;

namespace Ukraine.Identity.Options;

public class UkraineJwtAuthenticationOptions
{
	[Required]
	public required string Authority { get; set; }

	[Required]
	public required string Audience { get; set; }

	public bool RequireHttps { get; set; } = true;
}