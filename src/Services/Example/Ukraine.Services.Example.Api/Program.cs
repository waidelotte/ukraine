using HotChocolate;
using MediatR;
using Ukraine.Core.Extensions;
using Ukraine.Dapr.Extensions;
using Ukraine.EfCore.Extensions;
using Ukraine.GraphQl.Extenstion;
using Ukraine.HealthChecks.Extenstion;
using Ukraine.Identity.Extenstion;
using Ukraine.Logging.Extenstion;
using Ukraine.Services.Example.Api;
using Ukraine.Services.Example.Api.Graph.Mutations;
using Ukraine.Services.Example.Api.Graph.Queries;
using Ukraine.Services.Example.Infrastructure.Extensions;
using Ukraine.Services.Example.Infrastructure.Options;
using Ukraine.Services.Example.Persistence;
using Ukraine.Services.Example.Persistence.Extensions;
using Ukraine.Swagger.Extenstion;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var isDevelopment = builder.Environment.IsDevelopment();

configuration.AddUkraineDaprSecretStore("ukraine-secretstore");

services.AddInfrastructure(configuration);
services.AddPersistence(configuration);

builder.Host.AddUkraineSerilog(services, configuration.GetSection("UkraineLogging"));
builder.Host.AddUkraineServicesValidationOnBuild();

services.AddControllers();
services.AddUkraineSwagger(configuration.GetSection("UkraineSwagger"));

services.AddAuthorization(o =>
{
	o.AddScopePolicy(Constants.Policy.REST_API, new[] { Constants.Scope.REST_SCOPE });
	o.AddScopePolicy(Constants.Policy.GRAPHQL_API, new[] { Constants.Scope.GRAPHQL_SCOPE });
});

var authenticationOptions = services
	.BindAndGetOptions<AuthenticationOptions>(configuration.GetSection(Constants.ConfigurationSection.AUTHENTICATION));

services.AddJwtBearerAuthentication(o =>
{
	o.Audience = Constants.SERVICE_ID;
	o.Authority = authenticationOptions.Authority;
	o.RequireHttpsMetadata = !isDevelopment;

	o.TokenValidationParameters.ValidateAudience = true;
	o.TokenValidationParameters.ValidateIssuer = true;
	o.TokenValidationParameters.ValidateIssuerSigningKey = true;
});

services.AddUkraineGraphQl<ExampleContext>(configuration.GetSection("UkraineGraphQl"))
	.AddType<UserQueryTypeExtension>()
	.AddType<AuthorQueryTypeExtension>()
	.AddType<AuthorMutationTypeExtension>()
	.AddType<BookMutationTypeExtension>()
	.RegisterService<IMediator>(ServiceKind.Synchronized);

var app = builder.Build();

await app.Services.MigrateDatabaseAsync<ExampleContext>();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseUkraineSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.UseUkraineDaprEventBus();
app.UseUkraineGraphQl();
app.UseUkraineControllers();
app.UseUkraineHealthChecks();
app.UseUkraineDatabaseHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [service-example-api]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [service-example-api]");
}
finally
{
	Serilog.Log.CloseAndFlush();
}