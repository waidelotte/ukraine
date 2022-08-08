using FirstTestApiService;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.Telemetry.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Postgres");
Console.WriteLine("Connection String: " + connectionString);

builder.Services.AddCustomNpgsqlContext<Context, Context>(connectionString);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomTelemetry(builder.Configuration.GetValue<string>("ZipkinTelemetry:ServiceName"),o =>
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

app.Run();