using FluentValidation.AspNetCore;
using HotChocolate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.EventBus.Extensions;
using Ukraine.Infrastructure.Hosting.Extensions;
using Ukraine.Infrastructure.Identity.Extenstion;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Persistence.EfCore.Interfaces;
using Ukraine.Presentation.GraphQl.Extenstion;
using Ukraine.Presentation.HealthChecks.Extenstion;
using Ukraine.Presentation.Swagger.Extenstion;
using Ukraine.Services.Example.Api;
using Ukraine.Services.Example.Api.Graph.Mutations;
using Ukraine.Services.Example.Api.Graph.Queries;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Infrastructure.Extensions;
using Ukraine.Services.Example.Infrastructure.Options;
using Ukraine.Services.Example.Persistence;
using Ukraine.Services.Example.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Host.AddUkraineSerilog(services, configuration.GetSection("UkraineLogging"));
builder.Host.AddUkraineServicesValidationOnBuild();

var connectionString = builder.Configuration.GetConnectionString("Postgres");

if (string.IsNullOrEmpty(connectionString))
	throw ExampleException.Exception("Configuration: Postgres Connection String is null or empty");

builder.Services
	.AddInfrastructure(configuration)
	.AddPersistence(connectionString, builder.Configuration);

var identityOptions = builder.Configuration.GetRequiredSection<ExampleIdentityOptions>(ExampleIdentityOptions.SECTION_NAME);
var swaggerOptions = builder.Configuration.GetRequiredSection<ExampleSwaggerOptions>(ExampleSwaggerOptions.SECTION_NAME);

builder.Services.AddUkraineSwagger(options =>
{
	options.AddDoc(Constants.SERVICE_NAME);

	if (!string.IsNullOrEmpty(identityOptions.Authority))
		options.AddOAuth2(identityOptions.Authority, swaggerOptions.AuthScopes);

	options.AddAuthorizeFilter(swaggerOptions.AuthScopes);
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddControllers();

builder.Services
	.AddUkraineHealthChecks()
	.AddUkraineDaprHealthCheck()
	.AddUkrainePostgresHealthCheck(connectionString);

var graphQlOptions = builder.Configuration.GetRequiredSection<ExampleGraphQlOptions>(ExampleGraphQlOptions.SECTION_NAME);

builder.Services.AddUkraineGraphQl<ExampleContext>(options =>
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

builder.Services
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
	await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app
	.UseUkraineSwagger(options =>
	{
		options.AddEndpoint();

		if (!string.IsNullOrEmpty(swaggerOptions.OAuthClientId))
			options.AddOAuthClientId(swaggerOptions.OAuthClientId);

		options.AddOAuthAppName(Constants.SERVICE_NAME);
	})
	.UseUkraineAuthentication()
	.UseUkraineAuthorization()
	.UseCloudEvents();

app.MapSubscribeHandler();
app.UseUkraineGraphQl(options =>
{
	options.EnableBananaCakePop = graphQlOptions.EnableBananaCakePop;
});

app.MapControllers();
app.UseUkraineHealthChecks();
app.UseUkraineDatabaseHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [{ServiceName}]", Constants.SERVICE_NAME);
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", Constants.SERVICE_NAME);
}
finally
{
	Serilog.Log.CloseAndFlush();
}