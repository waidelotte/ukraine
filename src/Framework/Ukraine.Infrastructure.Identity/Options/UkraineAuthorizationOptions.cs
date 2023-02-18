namespace Ukraine.Infrastructure.Identity.Options;

public class UkraineAuthorizationOptions
{
	public Dictionary<string, string> ScopePolicies { get; set; } = new();
}