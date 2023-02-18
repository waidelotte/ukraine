using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ukraine.Presentation.Swagger.Filters;

internal sealed class AuthorizeCheckOperationFilter : IOperationFilter
{
	private readonly IEnumerable<string> _scopes;

	public AuthorizeCheckOperationFilter(IEnumerable<string> scopes)
	{
		_scopes = scopes;
	}

	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var methodAttributes = context.MethodInfo.GetCustomAttributes(true);

		var hasAuthorize = context.MethodInfo.DeclaringType?
			.GetCustomAttributes(true)
			.OfType<AuthorizeAttribute>().Any() ?? methodAttributes.OfType<AuthorizeAttribute>().Any();

		var allowAnonymous = methodAttributes.OfType<AllowAnonymousAttribute>().Any();

		if (!hasAuthorize || allowAnonymous)
			return;

		operation.Responses.TryAdd(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse
		{
			Description = Constants.Status.UNAUTHORIZED
		});

		operation.Responses.TryAdd(StatusCodes.Status403Forbidden.ToString(), new OpenApiResponse
		{
			Description = Constants.Status.FORBIDDEN
		});

		var oAuthScheme = new OpenApiSecurityScheme
		{
			Reference = new OpenApiReference
			{
				Type = ReferenceType.SecurityScheme,
				Id = SecuritySchemeType.OAuth2.ToString()
			}
		};

		operation.Security = new List<OpenApiSecurityRequirement>
		{
			new()
			{
				[oAuthScheme] = new List<string>(_scopes)
			}
		};
	}
}