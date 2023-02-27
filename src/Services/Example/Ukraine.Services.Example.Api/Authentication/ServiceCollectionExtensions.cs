﻿using Microsoft.AspNetCore.Authentication;
using Ukraine.Framework.Core.Authentication;
using Ukraine.Framework.Core.Options;

namespace Ukraine.Services.Example.Api.Authentication;

internal static class ServiceCollectionExtensions
{
	public static AuthenticationBuilder ConfigureAuthentication(
		this IServiceCollection serviceCollection, IConfiguration configuration)
	{
		var authenticationOptions = serviceCollection
			.BindAndGetOptions<ServiceAuthenticationOptions>(configuration.GetSection(ServiceAuthenticationOptions.CONFIGURATION_SECTION));

		return serviceCollection
			.AddJwtBearerAuthentication(options =>
			{
				options.Audience = Constants.SERVICE_ID;
				options.Authority = authenticationOptions.Authority;
				options.RequireHttpsMetadata = authenticationOptions.RequireHttpsMetadata;

				options.TokenValidationParameters.ValidateAudience = true;
				options.TokenValidationParameters.ValidateIssuer = true;
				options.TokenValidationParameters.ValidateIssuerSigningKey = true;
			});
	}
}