using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Framework.Core.Authentication;

public static class ServiceCollectionExtensions
{
	public static AuthenticationBuilder AddJwtBearerAuthentication(
		this IServiceCollection serviceCollection,
		Action<JwtBearerOptions> configureOptions)
	{
		return serviceCollection
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions);
	}
}