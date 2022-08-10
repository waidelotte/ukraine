using FirstTestApiService;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.HealthChecks;
using Ukraine.Infrastructure.Logging;
using Ukraine.Infrastructure.Swagger;
using Ukraine.Infrastructure.Telemetry;

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
builder.Services.AddCustomNpgsqlContext<Context, Context>(connectionString);
builder.Services.AddControllers();
builder.Services.AddCustomHealthChecks().AddCustomNpgSql(connectionString);
builder.Services.AddCustomTelemetry(serviceName,o =>
{
    o.Endpoint = builder.Configuration.GetValue<Uri>("ZipkinTelemetry:Endpoint");
});

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