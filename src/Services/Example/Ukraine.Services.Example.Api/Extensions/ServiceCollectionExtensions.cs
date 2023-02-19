using FluentValidation.AspNetCore;
using HotChocolate;
using MediatR;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Dapr.Extensions;
using Ukraine.Infrastructure.Identity.Extenstion;
using Ukraine.Presentation.GraphQl.Extenstion;
using Ukraine.Presentation.HealthChecks.Extenstion;
using Ukraine.Presentation.Swagger.Extenstion;
using Ukraine.Services.Example.Api.Graph.Mutations;
using Ukraine.Services.Example.Api.Graph.Queries;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Infrastructure.Extensions;
using Ukraine.Services.Example.Infrastructure.Options;
using Ukraine.Services.Example.Persistence;
using Ukraine.Services.Example.Persistence.Extensions;

namespace Ukraine.Services.Example.Api.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExampleApi(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Postgres");

		if (string.IsNullOrEmpty(connectionString))
			throw ExampleException.Exception("Configuration: Postgres Connection String is null or empty");

		services
			.AddInfrastructure(Constants.SERVICE_NAME, configuration)
			.AddPersistence(connectionString, configuration);

		var identityOptions = configuration.GetRequiredSection<ExampleIdentityOptions>(ExampleIdentityOptions.SECTION_NAME);
		var swaggerOptions = configuration.GetRequiredSection<ExampleSwaggerOptions>(ExampleSwaggerOptions.SECTION_NAME);

		services.AddUkraineSwagger(options =>
		{
			options.AddDoc(Constants.SERVICE_NAME);

			if (!string.IsNullOrEmpty(identityOptions.Authority))
				options.AddOAuth2(identityOptions.Authority, swaggerOptions.AuthScopes);

			options.AddAuthorizeFilter(swaggerOptions.AuthScopes);
		});

		services.AddFluentValidationAutoValidation();
		services.AddControllers();

		services
			.AddUkraineHealthChecks()
			.AddUkraineDaprHealthCheck()
			.AddUkrainePostgresHealthCheck(connectionString);

		var graphQlOptions = configuration.GetRequiredSection<ExampleGraphQlOptions>(ExampleGraphQlOptions.SECTION_NAME);

		services.AddUkraineGraphQl<ExampleContext>(options =>
			{
				options.IncludeExceptionDetails = graphQlOptions.IncludeExceptionDetails;
				options.AllowIntrospection = graphQlOptions.AllowIntrospection;
				options.IncludeInstrumentation = graphQlOptions.IncludeInstrumentation;
				options.ExecutionMaxDepth = graphQlOptions.ExecutionMaxDepth;
			})
			.AddType<AuthorQueryTypeExtension>()
			.AddType<AuthorMutationTypeExtension>()
			.AddType<BookMutationTypeExtension>()
			.RegisterService<IMediator>(ServiceKind.Synchronized);

		services
			.AddUkraineAuthorization(options =>
			{
				foreach (var policy in identityOptions.Policies)
				{
					if (string.IsNullOrEmpty(policy.Name))
						throw ExampleException.Exception("Policy Name is empty");

					options.AddScopePolicy(policy.Name, policy.Scopes);
				}
			})
			.AddUkraineJwtAuthentication(options =>
			{
				options.Audience = identityOptions.Audience;
				options.Authority = identityOptions.Authority;
				options.RequireHttps = identityOptions.RequireHttps;
			});

		return services;
	}
}