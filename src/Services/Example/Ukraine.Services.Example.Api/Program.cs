using Ukraine.Services.Example.Api;
using Ukraine.Services.Example.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExampleApi(builder.Configuration);
builder.Host.ConfigureHostApi(builder.Configuration);

var app = builder.Build();

app.UseExampleApi();

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