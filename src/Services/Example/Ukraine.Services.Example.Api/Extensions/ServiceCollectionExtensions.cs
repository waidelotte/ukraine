using FluentValidation.AspNetCore;
using HotChocolate;
using MediatR;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.EventBus.Dapr.Extensions;
using Ukraine.Infrastructure.GraphQl.Extenstion;
using Ukraine.Infrastructure.GraphQL.Extenstion;
using Ukraine.Infrastructure.HealthChecks.Extenstion;
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

		var connectionString = configuration.GetConnectionString("Postgres");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw ExampleException.Exception("Configuration: Postgres Connection String is null or empty");
		}

		var telemetryOptions = configuration.GetRequiredSection<ExampleTelemetryOptions>(ExampleTelemetryOptions.SECTION_NAME);

		services
			.AddInfrastructure(Constants.SERVICE_NAME, telemetry =>
			{
				telemetry.ZipkinServerUrl = telemetryOptions.ZipkinServerUrl;
				telemetry.RecordSqlException = telemetryOptions.RecordSqlException;
			})
			.AddInfrastructureEfCore(connectionString, options =>
			{
				options.RetryOnFailureDelay = databaseOptions.RetryOnFailureDelay;
				options.RetryOnFailureCount = databaseOptions.RetryOnFailureCount;
				options.DetailedErrors = databaseOptions.DetailedErrors;
				options.SensitiveDataLogging = databaseOptions.SensitiveDataLogging;
			});

		services.AddFluentValidationAutoValidation();
		services.AddControllers();

		services
			.AddUkraineHealthChecks()
			.AddUkraineDaprHealthCheck()
			.AddUkrainePostgresHealthCheck(connectionString);

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