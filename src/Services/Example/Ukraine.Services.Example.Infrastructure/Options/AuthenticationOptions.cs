using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Example.Infrastructure.Options;

public class AuthenticationOptions
{
	[Required]
	public required string Authority { get; set; }
}