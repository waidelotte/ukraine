using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Identity.Options;

namespace Ukraine.Infrastructure.Identity.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineAuthorization(
		this IServiceCollection serviceCollection,
		Action<UkraineAuthorizationOptionsBuilder>? configure = null)
	{
		var options = new UkraineAuthorizationOptionsBuilder();
		configure?.Invoke(options);

		var build = options.Build();

		return build != null ? serviceCollection.AddAuthorization(build) : serviceCollection.AddAuthorization();
	}

	public static AuthenticationBuilder AddUkraineJwtAuthentication(
		this IServiceCollection serviceCollection,
		Action<UkraineJwtAuthenticationOptionsBuilder>? configure = null)
	{
		var options = new UkraineJwtAuthenticationOptionsBuilder();
		configure?.Invoke(options);

		return serviceCollection
			.AddAuthentication(Constants.BEARER_NAME)
			.AddJwtBearer(Constants.BEARER_NAME, o =>
			{
				o.Audience = options.Audience;
				o.Authority = options.Authority;
				o.RequireHttpsMetadata = options.RequireHttps;
			});
	}
}