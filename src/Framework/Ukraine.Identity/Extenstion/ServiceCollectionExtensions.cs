using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Identity.Extenstion;

public static class ServiceCollectionExtensions
{
	public static AuthenticationBuilder AddJwtBearerAuthentication(
		this IServiceCollection serviceCollection,
		Action<JwtBearerOptions> options)
	{
		return serviceCollection
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options);
	}
}