using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EventBus.Dapr;
using Ukraine.Infrastructure.HealthChecks;
using Ukraine.Infrastructure.Logging;
using Ukraine.Infrastructure.Swagger;
using Ukraine.Infrastructure.Telemetry;
using Ukraine.Services.Example.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

var serviceName = builder.Configuration["ServiceName"];
var connectionString = builder.Configuration.GetConnectionString("Postgres");

builder.AddCustomLog(options =>
{
	options.ApplicationName = serviceName;
	options.WriteToSeq = true;
	options.SeqServerUrl = builder.Configuration["SeqServerUrl"];
});

builder.Services.AddCustomSwagger(serviceName);
builder.Services.AddCustomNpgsqlContext<ExampleContext, ExampleContext>(connectionString);
builder.Services.AddControllers();
builder.Services.AddCustomHealthChecks().AddCustomNpgSql(connectionString);
builder.Services.AddCustomTelemetry(o =>
{
	o.ApplicationName = builder.Configuration["ServiceName"];
	o.UseZipkin = true;
	o.ZipkinEndpoint = builder.Configuration["ZipkinTelemetry:Endpoint"];
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

app.UseCustomHealthChecks();

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