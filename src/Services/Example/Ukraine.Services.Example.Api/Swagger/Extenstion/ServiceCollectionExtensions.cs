using Microsoft.OpenApi.Models;
using Ukraine.Framework.Core.Options;
using Ukraine.Services.Example.Api.Swagger.Filters;
using Ukraine.Services.Example.Api.Swagger.Options;

namespace Ukraine.Services.Example.Api.Swagger.Extenstion;

internal static class ServiceCollectionExtensions
{
	public static IServiceCollection ConfigureSwagger(
		this IServiceCollection serviceCollection,
		IConfiguration configuration)
	{
		var swaggerOptions = serviceCollection
			.BindAndGetOptions<ServiceSwaggerOptions>(configuration.GetSection(ServiceSwaggerOptions.CONFIGURATION_SECTION));

		return serviceCollection.AddSwaggerGen(o =>
		{
			o.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo
			{
				Title = swaggerOptions.ServiceTitle,
				Version = swaggerOptions.Version
			});

			o.AddSecurityDefinition(SecuritySchemeType.OAuth2.ToString(), new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.OAuth2,
				Flows = new OpenApiOAuthFlows
				{
					Implicit = new OpenApiOAuthFlow
					{
						AuthorizationUrl = swaggerOptions.Security.AuthorizationUrl,
						TokenUrl = swaggerOptions.Security.TokenUrl,
						Scopes = swaggerOptions.Security.Scopes
					}
				}
			});

			o.OperationFilter<AuthorizeCheckOperationFilter>(swaggerOptions.Security.Scopes.Keys);
		});
	}
}