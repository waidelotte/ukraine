using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Hosting.Extensions;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Services.Example.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var serviceName = builder.Configuration.GetRequiredServiceName();

builder.Services.AddExampleApi(builder.Configuration, serviceName);

builder.Host
	.UseUkraineSerilog(serviceName, builder.Configuration.GetSection("LoggingOptions"))
	.UseUkraineServicesValidationOnBuild();

var app = builder.Build();

app.UseExampleApi();

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