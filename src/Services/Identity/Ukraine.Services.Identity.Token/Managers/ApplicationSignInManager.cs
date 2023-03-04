using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Ukraine.Services.Identity.Token.Managers
{
	public class ApplicationSignInManager<TUser> : SignInManager<TUser>
		where TUser : class
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public ApplicationSignInManager(
			UserManager<TUser> userManager,
			IHttpContextAccessor contextAccessor,
			IUserClaimsPrincipalFactory<TUser> claimsFactory,
			IOptions<IdentityOptions> optionsAccessor,
			ILogger<ApplicationSignInManager<TUser>> logger,
			IAuthenticationSchemeProvider schemes,
			IUserConfirmation<TUser> confirmation) : base(
			userManager,
			contextAccessor,
			claimsFactory,
			optionsAccessor,
			logger,
			schemes,
			confirmation)
		{
			_contextAccessor = contextAccessor;
		}

		public override async Task SignInWithClaimsAsync(
			TUser user,
			AuthenticationProperties? authenticationProperties,
			IEnumerable<Claim> additionalClaims)
		{
			var claims = additionalClaims.ToList();

			var result = await _contextAccessor.HttpContext!.AuthenticateAsync(IdentityConstants.ExternalScheme)!;

			if (result.Succeeded)
			{
				var sid = result.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);

				if (sid != null)
					claims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));

				if (authenticationProperties != null)
				{
					var idToken = result.Properties.GetTokenValue("id_token");

					if (idToken != null)
						authenticationProperties.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
				}

				var authenticationMethod = claims.FirstOrDefault(x => x.Type == ClaimTypes.AuthenticationMethod);
				var idp = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.IdentityProvider);

				if (authenticationMethod != null && idp == null)
				{
					claims.Add(new Claim(JwtClaimTypes.IdentityProvider, authenticationMethod.Value));
				}
			}

			await base.SignInWithClaimsAsync(user, authenticationProperties, claims);
		}
	}
}