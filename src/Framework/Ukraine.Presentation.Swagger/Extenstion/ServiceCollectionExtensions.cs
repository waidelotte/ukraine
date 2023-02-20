using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ukraine.Presentation.Swagger.Filters;
using Ukraine.Presentation.Swagger.Options;

namespace Ukraine.Presentation.Swagger.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineSwagger(
		this IServiceCollection serviceCollection,
		IConfigurationSection configurationSection)
	{
		serviceCollection.AddOptions<UkraineSwaggerOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var options = configurationSection.Get<UkraineSwaggerOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (options == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		return serviceCollection.AddSwaggerGen(o =>
		{
			o.SwaggerDoc(options.Version, new OpenApiInfo
			{
				Title = options.ServiceTitle,
				Version = options.Version
			});

			if (options.OAuth2Security != null)
			{
				o.AddSecurityDefinition(SecuritySchemeType.OAuth2.ToString(), new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						Implicit = new OpenApiOAuthFlow
						{
							AuthorizationUrl = options.OAuth2Security.AuthorizationUrl,
							TokenUrl = options.OAuth2Security.TokenUrl,
							Scopes = options.OAuth2Security.Scopes
						}
					}
				});

				o.OperationFilter<AuthorizeCheckOperationFilter>(options.OAuth2Security.Scopes.Keys);
			}
		});
	}
}