using Duende.IdentityServer.Models;

namespace Ukraine.Services.Identity.Persistence.Configuration;

public class IdentityServerData
{
	public IReadOnlyCollection<IdentityClient> Clients { get; set; } = Array.Empty<IdentityClient>();

	public IReadOnlyCollection<IdentityResource> IdentityResources { get; set; } = Array.Empty<IdentityResource>();

	public IReadOnlyCollection<ApiResource> ApiResources { get; set; } = Array.Empty<ApiResource>();

	public IReadOnlyCollection<ApiScope> ApiScopes { get; set; } = Array.Empty<ApiScope>();
}