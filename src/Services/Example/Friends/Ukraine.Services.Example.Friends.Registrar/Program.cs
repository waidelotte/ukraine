using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ukraine.Framework.Core.HealthChecks;
using Ukraine.Framework.Core.Host;
using Ukraine.Framework.Core.Serilog;
using Ukraine.Framework.Dapr;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Host.UseSerilog(configuration);
builder.Host.AddServicesValidationOnBuild();

services.AddDaprEventBus(options =>
{
	options.PubSubName = "ukraine-pubsub";
});

services.AddControllers();

services
	.AddHealthChecks()
	.AddCheck(
		"Service",
		() => HealthCheckResult.Healthy(),
		new[] { "service", "api", "demo" })
	.AddDaprHealthCheck(
		"Dapr Sidecar",
		HealthStatus.Unhealthy,
		new[] { "service", "dapr" });

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseCloudEvents();
app.MapSubscribeHandler();
app.MapControllers();
app.MapDefaultHealthChecks();

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