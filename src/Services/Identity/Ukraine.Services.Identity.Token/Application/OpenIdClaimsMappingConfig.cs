using Duende.IdentityServer.Hosting.DynamicProviders;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Ukraine.Services.Identity.Token.Application;

public class OpenIdClaimsMappingConfig : ConfigureAuthenticationOptions<OpenIdConnectOptions, OidcProvider>
{
	public OpenIdClaimsMappingConfig(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
	{
	}

	protected override void Configure(ConfigureAuthenticationContext<OpenIdConnectOptions, OidcProvider> context)
	{
		context.IdentityProvider.Properties.TryGetValue("MapInboundClaims", out var resultMapInboundClaims);

		var mapInboundClaims = resultMapInboundClaims is null or "true";

		context.AuthenticationOptions.MapInboundClaims = mapInboundClaims;
	}
}