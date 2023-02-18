namespace Ukraine.Services.Example.Api.Options;

public class ExampleIdentityPolicyOptions
{
	public string? Name { get; set; }

	public IEnumerable<string> Scopes { get; set; } = Array.Empty<string>();
}