namespace Ukraine.Services.Identity.Persistence.Configuration;

public class IdentityClient : Duende.IdentityServer.Models.Client
{
	public IReadOnlyCollection<IdentityClaim> ClientClaims { get; set; } = Array.Empty<IdentityClaim>();
}