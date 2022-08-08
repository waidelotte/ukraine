using FirstTestApiService;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
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

var connectionString = builder.Configuration.GetConnectionString("Postgres");
Console.WriteLine("Connection String: " + connectionString);

builder.Services.AddCustomNpgsqlContext<Context, Context>(connectionString);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomTelemetry(serviceName,o =>
{
    o.Endpoint = builder.Configuration.GetValue<Uri>("ZipkinTelemetry:Endpoint");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
Console.WriteLine("Database can connect: " + context.Database.EnsureCreated());

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