using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Web.Status.Options;

var builder = WebApplication.CreateBuilder(args);

var statusOptions = builder.Configuration.Get<StatusOptions>();
if (statusOptions == null) throw CoreException.Exception("Unable to initialize section: root");
if (string.IsNullOrEmpty(statusOptions.ResourcesPath)) throw CoreException.NullOrEmpty(nameof(statusOptions.ResourcesPath));
if (string.IsNullOrEmpty(statusOptions.UIPath)) throw CoreException.NullOrEmpty(nameof(statusOptions.UIPath));

var seqOptions = builder.Configuration.GetSection(SeqOptions.SectionName).Get<SeqOptions>();
if (seqOptions == null) throw CoreException.Exception($"Unable to initialize section: {SeqOptions.SectionName}");

builder.Host.AddCustomLog(builder.Configuration, options =>
{
    options.ApplicationName = statusOptions.ServiceName;
    options.WriteToSeq = seqOptions.IsEnabled;
    options.SeqServerUrl = seqOptions.ServerUrl;
});

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

app.MapGet("/", () => Results.LocalRedirect(statusOptions.UIPath));
app.MapHealthChecksUI(config => config.UIPath = statusOptions.UIPath);

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