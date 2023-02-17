using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ukraine.Presentation.Swagger.Filters;
using Ukraine.Presentation.Swagger.Options;

namespace Ukraine.Presentation.Swagger.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineSwagger(
		this IServiceCollection serviceCollection,
		Action<UkraineSwaggerOptions>? options = null)
	{
		var opt = new UkraineSwaggerOptions();
		options?.Invoke(opt);

		return serviceCollection.AddSwaggerGen(c =>
		{
			c.SwaggerDoc(Constants.DEFAULT_VERSION, new OpenApiInfo
			{
				Title = opt.Title,
				Version = opt.Version
			});

			if (!string.IsNullOrEmpty(opt.IdentityServerUrl))
			{
				c.AddSecurityDefinition(SecuritySchemeType.OAuth2.ToString(), new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						Implicit = new OpenApiOAuthFlow
						{
							AuthorizationUrl = new Uri(new Uri(opt.IdentityServerUrl), Constants.IDENTITY_AUTHORIZATION_URL),
							TokenUrl = new Uri(new Uri(opt.IdentityServerUrl), Constants.IDENTITY_TOKEN_URL),
							Scopes = opt.AuthScopes
						}
					}
				});
			}

			if (opt.AuthScopes.Any())
				c.OperationFilter<AuthorizeCheckOperationFilter>(opt.AuthScopes.Keys);
		});
	}
}