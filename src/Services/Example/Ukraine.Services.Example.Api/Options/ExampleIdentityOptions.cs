namespace Ukraine.Services.Example.Api.Options;

public class ExampleIdentityOptions
{
	public const string SECTION_NAME = "Identity";

	public string? Authority { get; set; }

	public string? Audience { get; set; }

	public bool RequireHttps { get; set; }

	public IEnumerable<ExampleIdentityPolicyOptions> Policies { get; set; } = Array.Empty<ExampleIdentityPolicyOptions>();
}