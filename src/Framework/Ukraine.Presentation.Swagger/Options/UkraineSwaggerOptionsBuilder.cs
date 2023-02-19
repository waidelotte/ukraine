using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Ukraine.Presentation.Swagger.Filters;

namespace Ukraine.Presentation.Swagger.Options;

public class UkraineSwaggerOptionsBuilder
{
	private Action<SwaggerGenOptions>? _action;

	public void AddDoc(string title = Constants.DEFAULT_TITLE, string version = Constants.DEFAULT_VERSION)
	{
		_action += options => options.SwaggerDoc(version, new OpenApiInfo
		{
			Title = title,
			Version = version
		});
	}

	public void AddOAuth2(string serverUrl, IEnumerable<string> scopes)
	{
		AddOAuth2(serverUrl, scopes.ToDictionary(s => s));
	}

	public void AddOAuth2(string serverUrl, Dictionary<string, string> scopes)
	{
		_action += options => options.AddSecurityDefinition(SecuritySchemeType.OAuth2.ToString(), new OpenApiSecurityScheme
		{
			Type = SecuritySchemeType.OAuth2,
			Flows = new OpenApiOAuthFlows
			{
				Implicit = new OpenApiOAuthFlow
				{
					AuthorizationUrl = new Uri(new Uri(serverUrl), Constants.IDENTITY_AUTHORIZATION_URL),
					TokenUrl = new Uri(new Uri(serverUrl), Constants.IDENTITY_TOKEN_URL),
					Scopes = scopes
				}
			}
		});
	}

	public void AddAuthorizeFilter(IEnumerable<string> scopes)
	{
		_action += options => options.OperationFilter<AuthorizeCheckOperationFilter>(scopes);
	}

	internal Action<SwaggerGenOptions>? Build()
	{
		return _action;
	}
}