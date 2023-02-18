using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ukraine.Infrastructure.Identity.Options;

namespace Ukraine.Infrastructure.Identity.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineAuthorization(
		this IServiceCollection serviceCollection,
		Action<UkraineAuthorizationOptions>? options = null)
	{
		var opt = new UkraineAuthorizationOptions();
		options?.Invoke(opt);

		return serviceCollection.AddAuthorization(o =>
		{
			foreach (var scopePolicy in opt.ScopePolicies)
			{
				o.AddPolicy(scopePolicy.Value, policy =>
				{
					policy.RequireAuthenticatedUser();
					policy.RequireClaim(Constants.SCOPE_NAME, scopePolicy.Key);
				});
			}
		});
	}

	public static AuthenticationBuilder AddUkraineJwtBearerAuthentication(
		this IServiceCollection serviceCollection,
		Action<UkraineJwtAuthenticationOptions>? options = null)
	{
		var opt = new UkraineJwtAuthenticationOptions();
		options?.Invoke(opt);

		return serviceCollection
			.AddAuthentication(Constants.BEARER_NAME)
			.AddJwtBearer(Constants.BEARER_NAME, o =>
			{
				o.Authority = opt.Authority;
				o.RequireHttpsMetadata = opt.RequireHttps;

				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = opt.ValidateAudience
				};
			});
	}
}