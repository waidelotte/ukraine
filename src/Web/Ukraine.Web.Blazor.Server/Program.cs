using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Ukraine.Framework.Core;
using Ukraine.Framework.Core.HealthChecks;
using Ukraine.Framework.Core.Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Host.UseSerilog(configuration);
builder.Host.AddServicesValidationOnBuild();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

services
	.AddHealthChecks()
	.AddDefaultCheck("Web", new[] { "Web", "Blazor", "Server" });

var app = builder.Build();

if (app.Environment.IsDevelopmentDocker())
{
	app.UseDeveloperExceptionPage();
	app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapDefaultHealthChecks();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

try
{
	app.Logger.LogInformation("Starting Web Host [web-blazor]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [web-blazor]");
}
finally
{
	Serilog.Log.CloseAndFlush();
}