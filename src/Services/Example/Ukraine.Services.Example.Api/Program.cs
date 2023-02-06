using FluentValidation.AspNetCore;
using HotChocolate.AspNetCore;
using HotChocolate.Types.Pagination;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EventBus.Dapr.Extenstion;
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

var applicationOptions = builder.Configuration.Get<ExampleApplicationOptions>();
if (applicationOptions == null) throw ExampleException.Exception("Unable to initialize section: root");

var loggingOptions = builder.Configuration.GetSection(ExampleLoggingOptions.SectionName).Get<ExampleLoggingOptions>();
if (loggingOptions == null) throw ExampleException.Exception($"Unable to initialize section: {ExampleLoggingOptions.SectionName}");

var telemetryOptions = builder.Configuration.GetSection(ExampleTelemetryOptions.SectionName).Get<ExampleTelemetryOptions>();
if (telemetryOptions == null) throw ExampleException.Exception($"Unable to initialize section: {ExampleTelemetryOptions.SectionName}");

var healthCheckOptions = builder.Configuration.GetSection(ExampleHealthCheckOptions.SectionName).Get<ExampleHealthCheckOptions>();
if (healthCheckOptions == null) throw ExampleException.Exception($"Unable to initialize section: {ExampleHealthCheckOptions.SectionName}");

var databaseOptions = builder.Configuration.GetSection(ExampleDatabaseOptions.SectionName).Get<ExampleDatabaseOptions>();
if (databaseOptions == null) throw ExampleException.Exception($"Unable to initialize section: {ExampleDatabaseOptions.SectionName}");

var connectionString = builder.Configuration.GetConnectionString("Postgres");
if (string.IsNullOrEmpty(connectionString)) throw ExampleException.Exception("Unable to initialize section: connectionString");

builder.Host.AddCustomLog(builder.Configuration, options =>
{
	options.ApplicationName = applicationOptions.ServiceName;
	options.WriteToConsole = loggingOptions.WriteToConsole;
	options.WriteToSeq = loggingOptions.WriteToSeq;
	options.UseSerilog = loggingOptions.UseSerilog;
	options.SeqServerUrl = loggingOptions.SeqServerUrl;
});

builder.Services.AddCustomSwagger(options =>
{
	options.ServiceName = applicationOptions.ServiceName;
});
builder.Services.AddExampleInfrastructure(builder.Configuration);
builder.Services.AddExampleInfrastructureEfCore(connectionString, databaseOptions);
builder.Services.AddControllers();

if (healthCheckOptions.IsEnabled)
{
	var healthCheckBuilder = builder.Services.AddCustomHealthChecks();
	
	if(healthCheckOptions.CheckDatabase) healthCheckBuilder.AddCustomNpgSql(connectionString);
	if(healthCheckOptions.CheckDaprSidecar) healthCheckBuilder.AddCustomDaprHealthCheck();
}

builder.Services.AddCustomTelemetry(o =>
{
	o.ApplicationName = applicationOptions.ServiceName;
	o.UseZipkin = telemetryOptions.UseZipkin;
	o.ZipkinEndpoint = telemetryOptions.ZipkinServerUrl;
});

builder.Services.AddCustomDapr();
builder.Host.ValidateServicesOnBuild();

builder.Services.AddFluentValidationAutoValidation();

builder.Services
	.AddGraphQLServer()
	.RegisterDbContext<ExampleContext>()
	.AddProjections()
	.AddSorting()
	.AddQueryType(q => q.Name(OperationTypeNames.Query))
		.AddType<AuthorQueryTypeExtension>()
	.AddMutationType(q => q.Name(OperationTypeNames.Mutation))
		.AddType<AuthorMutationTypeExtension>()
		.AddType<BookMutationTypeExtension>()
	.AddMutationConventions()
	.ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
	.SetPagingOptions(new PagingOptions
	{
		MaxPageSize = 100,
		DefaultPageSize = 10,
		IncludeTotalCount = true,
		AllowBackwardPagination = false
	})
	.AllowIntrospection(builder.Environment.IsDevelopment());

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
	var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
	context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseCustomSwagger();

app.UseAuthorization();

app.UseCloudEvents();

app.MapGet("/", () => Results.LocalRedirect("~/graphql/ui"));

app.MapSubscribeHandler();

app.MapGraphQL().WithOptions(new GraphQLServerOptions
{
	EnableSchemaRequests = false,
	EnableGetRequests = false,
	EnableMultipartRequests = true,
	Tool = { Enable = app.Environment.IsDevelopment() }
});

app.MapControllers();

if (healthCheckOptions.IsEnabled)
{
	app.UseCustomHealthChecks();
	if(healthCheckOptions.CheckDatabase) app.UseCustomDatabaseHealthChecks();
}

try
{
	app.Logger.LogInformation("Starting Web Host [{ServiceName}]", applicationOptions.ServiceName);
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", applicationOptions.ServiceName);
}
finally
{
	Serilog.Log.CloseAndFlush();
}