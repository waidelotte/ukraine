using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EventBus.Dapr.Extenstion;
using Ukraine.Infrastructure.HealthChecks.Extenstion;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Infrastructure.Swagger.Extenstion;
using Ukraine.Infrastructure.Telemetry.Extenstion;
using Ukraine.Services.Example.Infrastructure.EfCore.Extensions;
using Ukraine.Services.Example.Infrastructure.EfCore.Options;
using Ukraine.Services.Example.Infrastructure.Extensions;
using Ukraine.Services.Example.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

var applicationOptions = builder.Configuration.Get<ExampleApplicationOptions>();
if (applicationOptions == null) throw CoreException.Exception("Unable to initialize section: root");

var loggingOptions = builder.Configuration.GetSection(ExampleLoggingOptions.SectionName).Get<ExampleLoggingOptions>();
if (loggingOptions == null) throw CoreException.Exception($"Unable to initialize section: {ExampleLoggingOptions.SectionName}");

var telemetryOptions = builder.Configuration.GetSection(ExampleTelemetryOptions.SectionName).Get<ExampleTelemetryOptions>();
if (telemetryOptions == null) throw CoreException.Exception($"Unable to initialize section: {ExampleTelemetryOptions.SectionName}");

var healthCheckOptions = builder.Configuration.GetSection(ExampleHealthCheckOptions.SectionName).Get<ExampleHealthCheckOptions>();
if (healthCheckOptions == null) throw CoreException.Exception($"Unable to initialize section: {ExampleHealthCheckOptions.SectionName}");

var databaseOptions = builder.Configuration.GetSection(ExampleDatabaseOptions.SectionName).Get<ExampleDatabaseOptions>();
if (databaseOptions == null) throw CoreException.Exception($"Unable to initialize section: {ExampleDatabaseOptions.SectionName}");

var connectionString = builder.Configuration.GetConnectionString("Postgres");
if (string.IsNullOrEmpty(connectionString)) throw CoreException.Exception("Unable to initialize section: connectionString");

builder.AddCustomLog(options =>
{
	options.ApplicationName = applicationOptions.ServiceName;
	options.WriteToConsole = loggingOptions.WriteToConsole;
	options.WriteToSeq = loggingOptions.WriteToSeq;
	options.UseSerilog = loggingOptions.UseSerilog;
	options.SeqServerUrl = loggingOptions.SeqServerUrl;
});

builder.Services.AddCustomSwagger(options =>
{
	options.ApplicationName = applicationOptions.ServiceName;
});
builder.Services.AddExampleInfrastructure(builder.Configuration);
builder.Services.AddExampleInfrastructureEfCore(connectionString, databaseOptions);
builder.Services.AddControllers();

if (healthCheckOptions.IsEnabled)
{
	var healthCheckBuilder = builder.Services.AddCustomHealthChecks();
	if(healthCheckOptions.CheckDatabase) healthCheckBuilder.AddCustomNpgSql(connectionString);
}

builder.Services.AddCustomTelemetry(o =>
{
	o.ApplicationName = applicationOptions.ServiceName;
	o.UseZipkin = telemetryOptions.UseZipkin;
	o.ZipkinEndpoint = telemetryOptions.ZipkinServerUrl;
});

builder.Services.AddCustomDapr();

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
	var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
	context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseCustomSwagger();

app.UseAuthorization();

app.MapGet("/", () => Results.LocalRedirect("~/swagger"));

app.MapSubscribeHandler();

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