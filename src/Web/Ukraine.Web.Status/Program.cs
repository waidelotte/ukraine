using Ukraine.Framework.Core.Options;
using Ukraine.Web.Status.Options;

var builder = WebApplication.CreateBuilder(args);

var webStatusOptions = builder.Configuration.GetRequiredSection("WebStatus").GetOptions<WebStatusOptions>();

builder.Services
	.AddHealthChecksUI(settings =>
	{
		settings.SetEvaluationTimeInSeconds(webStatusOptions.EvaluationTimeInSeconds);
		settings.SetApiMaxActiveRequests(webStatusOptions.MaxActiveRequests);
		settings.MaximumHistoryEntriesPerEndpoint(webStatusOptions.MaximumHistoryEntriesPerEndpoint);
	})
	.AddInMemoryStorage();

var app = builder.Build();

app.UseHealthChecksUI(config =>
{
	config.ResourcesPath = "/ui/resources";
});

app.MapGet("/", () => Results.LocalRedirect("/hc-ui"));
app.MapHealthChecksUI(config => config.UIPath = "/hc-ui");

try
{
	app.Logger.LogInformation("Starting Web Host [web-status]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [web-status]");
}
finally
{
	Serilog.Log.CloseAndFlush();
}