using FluentValidation.AspNetCore;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.EfCore.GraphQL.Extensions;
using Ukraine.Infrastructure.EventBus.Dapr.Extenstions;
using Ukraine.Infrastructure.GraphQL.Extenstion;
using Ukraine.Infrastructure.HealthChecks.Extenstion;
using Ukraine.Infrastructure.Swagger.Extenstion;
using Ukraine.Infrastructure.Telemetry.Extenstion;
using Ukraine.Services.Example.Api.Graph.Mutations;
using Ukraine.Services.Example.Api.Graph.Queries;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Infrastructure.EfCore;
using Ukraine.Services.Example.Infrastructure.EfCore.Extensions;
using Ukraine.Services.Example.Infrastructure.EfCore.Options;
using Ukraine.Services.Example.Infrastructure.Extensions;
using Ukraine.Services.Example.Infrastructure.Options;

namespace Ukraine.Services.Example.Api.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExampleApi(this IServiceCollection services, IConfiguration configuration, string serviceName)
	{
		var connectionString = configuration.GetConnectionString("Postgres");
		
		if (string.IsNullOrEmpty(connectionString)) 
			throw ExampleException.Exception("Unable to initialize section: connectionString");
		
		var zipkinServerUrl = configuration.GetSection("TelemetryOptions").GetRequiredValue<string>("ZipkinServerUrl");
		var databaseOptions = configuration.GetRequiredOption<ExampleDatabaseOptions>("DatabaseOptions");
		var graphQlOptions = configuration.GetRequiredOption<ExampleGraphQLOptions>("GraphQLOptions");

		services.Configure<ExampleGraphQLOptions>(configuration.GetSection("GraphQLOptions"));
		
		services
			.AddExampleInfrastructure()
			.AddExampleInfrastructureEfCore(connectionString, databaseOptions)
			.AddUkraineSwagger(serviceName)
			.AddUkraineZipkinTelemetry(serviceName, zipkinServerUrl)
			.AddUkraineDaprEventBus()
			.AddFluentValidationAutoValidation();
		
		services.AddControllers();
		
		services
			.AddUkraineHealthChecks()
			.AddUkrainePostgresHealthCheck(connectionString)
			.AddUkraineDaprHealthCheck();
		
		services.AddUkraineGraphQL(options =>
			{
				options.IncludeExceptionDetails = graphQlOptions.IncludeExceptionDetails;
				options.UseIntrospection = graphQlOptions.IsIntrospectionEnabled;
				options.DefaultPageSize = graphQlOptions.Paging.DefaultPageSize;
				options.MaxPageSize = graphQlOptions.Paging.MaxPageSize;
			}).AddUkraineGraphQLInstrumentation().AddUkraineEfCore<ExampleContext>()
			.AddQueryType(q => q.Name(OperationTypeNames.Query))
			.AddType<AuthorQueryTypeExtension>()
			.AddMutationType(q => q.Name(OperationTypeNames.Mutation))
			.AddType<AuthorMutationTypeExtension>()
			.AddType<BookMutationTypeExtension>()
			.RegisterService<IMediator>(ServiceKind.Synchronized);
		
		return services;
	}
}