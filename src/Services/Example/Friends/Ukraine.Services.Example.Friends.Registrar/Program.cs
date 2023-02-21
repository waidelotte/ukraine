using Ukraine.Infrastructure.EventBus.Extensions;
using Ukraine.Infrastructure.Extensions;
using Ukraine.Logging.Extenstion;
using Ukraine.HealthChecks.Extenstion;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Host.AddUkraineSerilog(services, configuration.GetSection("UkraineLogging"));
builder.Host.AddUkraineServicesValidationOnBuild();

services.AddUkraineDaprEventBus(configuration.GetSection("UkraineEventBus"));
services.AddUkraineControllers();
services
	.AddUkraineHealthChecks()
	.AddUkraineDaprHealthCheck()
	.AddUkraineServiceCheck();

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseUkraineDaprEventBus();
app.UseUkraineHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [service-example-friend-registrar]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [service-example-friend-registrar]");
}
finally
{
	Serilog.Log.CloseAndFlush();
}