using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Persistence.Configuration;

public class IdentityClaim
{
	[Required]
	public required string Type { get; set; }

	[Required]
	public required string Value { get; set; }
}