﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Identity.Options;

namespace Ukraine.Identity.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineAuthorization(
		this IServiceCollection serviceCollection,
		IConfigurationSection configurationSection)
	{
		serviceCollection.AddOptions<UkraineAuthorizationOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var options = configurationSection.Get<UkraineAuthorizationOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (options == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		return serviceCollection.AddAuthorization(o =>
		{
			if (options.Policies != null)
			{
				foreach (var policy in options.Policies)
				{
					o.AddPolicy(policy.Name, builder =>
					{
						if (policy.RequireAuthenticatedUser)
							builder.RequireAuthenticatedUser();

						foreach (var scope in policy.Scopes)
						{
							builder.RequireClaim("scope", scope);
						}
					});
				}
			}
		});
	}

	public static AuthenticationBuilder AddUkraineJwtAuthentication(
		this IServiceCollection serviceCollection,
		IConfigurationSection configurationSection)
	{
		serviceCollection.AddOptions<UkraineJwtAuthenticationOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var options = configurationSection.Get<UkraineJwtAuthenticationOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (options == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		return serviceCollection
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
			{
				o.Audience = options.Audience;
				o.Authority = options.Authority;
				o.RequireHttpsMetadata = options.RequireHttps;

				o.TokenValidationParameters.ValidateAudience = true;
				o.TokenValidationParameters.ValidateIssuer = true;
				o.TokenValidationParameters.ValidateIssuerSigningKey = true;
			});
	}
}