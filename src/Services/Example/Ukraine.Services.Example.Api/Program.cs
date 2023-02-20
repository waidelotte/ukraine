using HotChocolate;
using MediatR;
using Ukraine.Infrastructure.EventBus.Extensions;
using Ukraine.Infrastructure.Extensions;
using Ukraine.Infrastructure.Identity.Extenstion;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Persistence.EfCore.Extensions;
using Ukraine.Presentation.GraphQl.Extenstion;
using Ukraine.Presentation.HealthChecks.Extenstion;
using Ukraine.Presentation.Swagger.Extenstion;
using Ukraine.Services.Example.Api;
using Ukraine.Services.Example.Api.Graph.Mutations;
using Ukraine.Services.Example.Api.Graph.Queries;
using Ukraine.Services.Example.Infrastructure.Extensions;
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

services.AddUkraineGraphQl<ExampleContext>(configuration.GetSection("UkraineGraphQl"))
	.AddType<AuthorQueryTypeExtension>()
	.AddType<AuthorMutationTypeExtension>()
	.AddType<BookMutationTypeExtension>()
	.RegisterService<IMediator>(ServiceKind.Synchronized);

services
	.AddUkraineAuthorization(configuration.GetSection("UkraineAuthorization"))
	.AddUkraineJwtAuthentication(configuration.GetSection("UkraineJwtAuthentication"));

var app = builder.Build();

await app.Services.MigrateDatabaseAsync();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseUkraineSwagger();
app.UseUkraineAuthentication();
app.UseUkraineAuthorization();
app.UseUkraineDaprEventBus();
app.UseUkraineGraphQl();
app.UseUkraineControllers();
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