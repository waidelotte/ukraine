using Ukraine.Framework.Core.HealthChecks;
using Ukraine.Framework.Core.Host;
using Ukraine.Framework.Core.Serilog;
using Ukraine.Framework.Dapr;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Api.Authentication;
using Ukraine.Services.Example.Api.Authorization;
using Ukraine.Services.Example.Api.GraphQl.Extensions;
using Ukraine.Services.Example.Api.HealthChecks;
using Ukraine.Services.Example.Api.Swagger.Extenstion;
using Ukraine.Services.Example.Api.Telemetry;
using Ukraine.Services.Example.Infrastructure.Extensions;
using Ukraine.Services.Example.Persistence;
using Ukraine.Services.Example.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

configuration.AddDaprSecretStore("ukraine-secretstore");

services.AddInfrastructure();
services.AddPersistence(configuration);

builder.Host.UseSerilog(configuration);
builder.Host.AddServicesValidationOnBuild();

services.AddControllers();
services.AddHttpContextAccessor();

services.ConfigureSwagger(configuration);
services.ConfigureAuthorization();
services.ConfigureAuthentication(configuration);
services.ConfigureGraphQl(configuration);
services.ConfigureHealthChecks(configuration);
services.ConfigureTelemetry(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	await app.Services.MigrateDatabaseAsync<ExampleContext>();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCloudEvents();
app.MapSubscribeHandler();
app.UseServiceSwagger();
app.UseServiceGraphQl();
app.MapControllers();
app.MapDefaultHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [service-example]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [service-example]");
}
finally
{
	Serilog.Log.CloseAndFlush();
}