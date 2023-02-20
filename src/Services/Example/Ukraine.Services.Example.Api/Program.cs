using HotChocolate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.Extensions;
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

services.AddInfrastructure(configuration);
services.AddPersistence(configuration);

builder.Host.AddUkraineSerilog(services, configuration.GetSection("UkraineLogging"));
builder.Host.AddUkraineServicesValidationOnBuild();

services.AddUkraineControllers();
services.AddUkraineSwagger(configuration.GetSection("UkraineSwagger"));

builder.Services.AddUkraineGraphQl<ExampleContext>(configuration.GetSection("UkraineGraphQl"))
	.AddType<AuthorQueryTypeExtension>()
	.AddType<AuthorMutationTypeExtension>()
	.AddType<BookMutationTypeExtension>()
	.RegisterService<IMediator>(ServiceKind.Synchronized);

var identityOptions = builder.Configuration.GetRequiredSection<ExampleIdentityOptions>(ExampleIdentityOptions.SECTION_NAME);

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
	.UseUkraineSwagger()
	.UseUkraineAuthentication()
	.UseUkraineAuthorization()
	.UseCloudEvents();

app.MapSubscribeHandler();
app.UseUkraineGraphQl();
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