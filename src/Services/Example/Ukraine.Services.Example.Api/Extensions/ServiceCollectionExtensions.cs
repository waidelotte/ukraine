using FluentValidation.AspNetCore;
using HotChocolate;
using MediatR;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.EfCore.GraphQL.Extensions;
using Ukraine.Infrastructure.EventBus.Dapr.Extensions;
using Ukraine.Infrastructure.GraphQL.Extenstion;
using Ukraine.Infrastructure.HealthChecks.Extenstion;
using Ukraine.Infrastructure.Swagger.Extenstion;
using Ukraine.Infrastructure.Telemetry.Extenstion;
using Ukraine.Services.Example.Api.Graph.Mutations;
using Ukraine.Services.Example.Api.Graph.Queries;
using Ukraine.Services.Example.Api.Options;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Infrastructure.EfCore;
using Ukraine.Services.Example.Infrastructure.EfCore.Extensions;
using Ukraine.Services.Example.Infrastructure.Extensions;

namespace Ukraine.Services.Example.Api.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExampleApi(this IServiceCollection services, IConfiguration configuration)
	{
		var databaseOptions = configuration.GetRequiredSection<ExampleDatabaseOptions>(ExampleDatabaseOptions.SECTION_NAME);

		var telemetryOptions = configuration.GetRequiredSection<ExampleTelemetryOptions>(ExampleTelemetryOptions.SECTION_NAME);

		if (string.IsNullOrEmpty(telemetryOptions.ZipkinServerUrl))
		{
			throw ExampleException.Exception($"Configuration: {nameof(telemetryOptions.ZipkinServerUrl)} is null or empty");
		}

		var connectionString = configuration.GetConnectionString("Postgres");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw ExampleException.Exception("Configuration: Postgres Connection String is null or empty");
		}

		services
			.AddInfrastructure()
			.AddInfrastructureEfCore(connectionString, options =>
			{
				options.RetryOnFailureDelay = databaseOptions.RetryOnFailureDelay;
				options.RetryOnFailureCount = databaseOptions.RetryOnFailureCount;
			})
			.AddUkraineSwagger(Constants.SERVICE_NAME)
			.AddUkraineZipkinTelemetry(Constants.SERVICE_NAME, telemetryOptions.ZipkinServerUrl)
			.AddUkraineDaprEventBus()
			.AddFluentValidationAutoValidation();

		services.AddControllers();

		services
			.AddUkraineHealthChecks()
			.AddUkrainePostgresHealthCheck(connectionString)
			.AddUkraineDaprHealthCheck();

		var graphQlOptions = configuration.GetRequiredSection<ExampleGraphQlOptions>(ExampleGraphQlOptions.SECTION_NAME);

		services.AddUkraineGraphQl(options =>
			{
				options.IncludeExceptionDetails = graphQlOptions.IncludeExceptionDetails;
				options.UseIntrospection = graphQlOptions.UseIntrospection;
				options.UseInstrumentation = graphQlOptions.UseInstrumentation;
				options.DefaultPageSize = graphQlOptions.DefaultPageSize;
				options.MaxPageSize = graphQlOptions.MaxPageSize;
				options.MaxDepth = graphQlOptions.MaxDepth;
			})
			.AddUkraineEfCore<ExampleContext>()
			.AddType<AuthorQueryTypeExtension>()
			.AddType<AuthorMutationTypeExtension>()
			.AddType<BookMutationTypeExtension>()
			.RegisterService<IMediator>(ServiceKind.Synchronized);

		return services;
	}
}