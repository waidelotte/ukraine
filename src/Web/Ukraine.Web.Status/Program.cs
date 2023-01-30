using Ukraine.Infrastructure.Logging;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Web.Status.Options;

var builder = WebApplication.CreateBuilder(args);

var statusOptions = builder.Configuration.Get<StatusOptions>();
var seqOptions = builder.Configuration.GetSection(SeqOptions.Position).Get<SeqOptions>();

builder.AddCustomLog(options =>
{
    options.ApplicationName = statusOptions.ApplicationName;
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
    app.Logger.LogInformation("Starting Web Host [{ServiceName}]", statusOptions.ApplicationName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", statusOptions.ApplicationName);
}
finally
{
    Serilog.Log.CloseAndFlush();
}