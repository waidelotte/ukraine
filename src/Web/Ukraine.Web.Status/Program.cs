using Ukraine.Domain.Exceptions;
using Ukraine.Web.Status.Options;

var builder = WebApplication.CreateBuilder(args);

var statusOptions = builder.Configuration.Get<StatusOptions>();
if (statusOptions == null)
{
	throw CoreException.Exception("Unable to initialize section: root");
}

if (string.IsNullOrEmpty(statusOptions.ResourcesPath))
{
	throw CoreException.NullOrEmpty(nameof(statusOptions.ResourcesPath));
}

if (string.IsNullOrEmpty(statusOptions.UiPath))
{
	throw CoreException.NullOrEmpty(nameof(statusOptions.UiPath));
}

var seqOptions = builder.Configuration.GetSection(SeqOptions.SectionName).Get<SeqOptions>();
if (seqOptions == null)
{
	throw CoreException.Exception($"Unable to initialize section: {SeqOptions.SectionName}");
}

// builder.Host.AddUkraineSerilog(statusOptions.ServiceName!, builder.Configuration.GetSection("ApplicationLogging"));
builder.Services
	.AddHealthChecksUI(settings =>
	{
		settings.SetEvaluationTimeInSeconds(statusOptions.EvaluationTimeInSeconds);
		settings.SetApiMaxActiveRequests(statusOptions.MaxActiveRequests);
		settings.MaximumHistoryEntriesPerEndpoint(statusOptions.MaximumHistoryEntriesPerEndpoint);
	})
	.AddInMemoryStorage();

var app = builder.Build();

app.UseHealthChecksUI(config =>
{
	config.ResourcesPath = statusOptions.ResourcesPath;
});

app.MapGet("/", () => Results.LocalRedirect(statusOptions.UiPath));
app.MapHealthChecksUI(config => config.UIPath = statusOptions.UiPath);

try
{
	app.Logger.LogInformation("Starting Web Host [{ServiceName}]", statusOptions.ServiceName);
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", statusOptions.ServiceName);
}
finally
{
	Serilog.Log.CloseAndFlush();
}