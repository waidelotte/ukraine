using FluentValidation.AspNetCore;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.EfCore.GraphQL.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EventBus.Dapr.Extenstions;
using Ukraine.Infrastructure.GraphQL.Extenstion;
using Ukraine.Infrastructure.HealthChecks.Extenstion;
using Ukraine.Infrastructure.Hosting.Extensions;
using Ukraine.Infrastructure.Logging.Extenstion;
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

var builder = WebApplication.CreateBuilder(args);

var serviceName = builder.Configuration.GetServiceName();
var telemetryOptions = builder.Configuration.GetOption<ExampleTelemetryOptions>(ExampleTelemetryOptions.SectionName);
var databaseOptions = builder.Configuration.GetOption<ExampleDatabaseOptions>(ExampleDatabaseOptions.SectionName);
var graphQlOptions = builder.Configuration.GetOption<ExampleGraphQLOptions>(ExampleGraphQLOptions.SectionName);

var connectionString = builder.Configuration.GetConnectionString("Postgres");
if (string.IsNullOrEmpty(connectionString)) throw ExampleException.Exception("Unable to initialize section: connectionString");

builder.Host
	.AddUkraineSerilog(serviceName, builder.Configuration.GetSection("ApplicationLogging"))
	.AddUkraineServicesValidationOnBuild();

builder.Services
	.AddUkraineSwagger(serviceName)
	.AddExampleInfrastructure()
	.AddExampleInfrastructureEfCore(connectionString, databaseOptions)
	.AddUkraineZipkinTelemetry(serviceName, telemetryOptions.ZipkinServerUrl!)
	.AddUkraineDaprEventBus()
	.AddFluentValidationAutoValidation();

builder.Services.AddControllers();

builder.Services
	.AddUkraineHealthChecks()
	.AddUkrainePostgresHealthCheck(connectionString)
	.AddUkraineDaprHealthCheck();

builder.Services.AddUkraineGraphQL(options =>
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

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
	var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
	context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app
	.UseUkraineSwagger()
	.UseAuthorization()
	.UseCloudEvents();

app.MapGet("/", () => Results.LocalRedirect("~/graphql"));

app.MapSubscribeHandler();

app.UseUkraineGraphQL(graphQlOptions.IsToolEnabled);

app.MapControllers();

app.UseUkraineHealthChecks();
app.UseUkraineDatabaseHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [{ServiceName}]", serviceName);
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", serviceName);
}
finally
{
	Serilog.Log.CloseAndFlush();
}