namespace Ukraine.Infrastructure.Identity.Options;

public class UkraineAuthorizationOptions
{
	public IEnumerable<UkraineAuthorizationPolicyOptions>? Policies { get; set; }
}