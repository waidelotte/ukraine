using FirstTestApiService;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.Extensions;
using Ukraine.Infrastructure.Telemetry.Extensions;

var builder = WebApplication.CreateBuilder(args);

var serviceName = builder.Configuration["ServiceName"];

builder.AddCustomLog(options =>
{
    options.ApplicationName = serviceName;
    options.WriteToSeq = true;
    options.SeqServerUrl = builder.Configuration["SeqServerUrl"];
});
builder.Services.AddCustomSwagger(serviceName);
builder.Services.AddCustomNpgsqlContext<Context, Context>(builder.Configuration.GetConnectionString("Postgres"));
builder.Services.AddControllers();
builder.Services.AddCustomTelemetry(serviceName,o =>
{
    o.Endpoint = builder.Configuration.GetValue<Uri>("ZipkinTelemetry:Endpoint");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCustomSwagger();
app.UseAuthorization();
app.MapControllers();

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