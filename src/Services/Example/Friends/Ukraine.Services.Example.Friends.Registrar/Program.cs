using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.EventBus.Dapr.Extensions;
using Ukraine.Infrastructure.Hosting.Extensions;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Presentation.HealthChecks.Extenstion;
using Ukraine.Services.Example.Friends.Registrar;
using Ukraine.Services.Example.Friends.Registrar.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUkraineDaprEventBus();
builder.Services.AddControllers();
builder.Services
	.AddUkraineHealthChecks()
	.AddUkraineDaprHealthCheck();

var loggingOptions = builder.Configuration.GetRequiredSection<RegistrarLoggingOptions>(RegistrarLoggingOptions.SECTION_NAME);

builder.Host
	.UseUkraineSerilog(Constants.SERVICE_NAME, options =>
	{
		options.MinimumLevel = loggingOptions.MinimumLevel;
		options.Override(loggingOptions.Override);

		options.WriteTo = writeOptions =>
		{
			writeOptions.WriteToConsole = loggingOptions.WriteToConsole;
			writeOptions.WriteToSeqServerUrl = loggingOptions.WriteToSeqServerUrl;
		};
	})
	.UseUkraineServicesValidationOnBuild();

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseCloudEvents();
app.MapSubscribeHandler();
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