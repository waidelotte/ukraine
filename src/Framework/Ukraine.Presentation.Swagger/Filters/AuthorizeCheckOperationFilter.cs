﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ukraine.Presentation.Swagger.Filters;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
	private readonly IEnumerable<string> _scopes;

	public AuthorizeCheckOperationFilter(IEnumerable<string> scopes)
	{
		_scopes = scopes;
	}

	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var hasAuthorize = context.MethodInfo.DeclaringType?
			.GetCustomAttributes(true)
			.OfType<AuthorizeAttribute>().Any() ??
						   context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

		if (!hasAuthorize)
			return;

		operation.Responses.TryAdd(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse
		{
			Description = "Unauthorized"
		});

		operation.Responses.TryAdd(StatusCodes.Status403Forbidden.ToString(), new OpenApiResponse
		{
			Description = "Forbidden"
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