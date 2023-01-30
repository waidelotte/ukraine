using Ukraine.Gateways.Http.Options;
using Ukraine.Infrastructure.Logging;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Infrastructure.Telemetry;
using Ukraine.Infrastructure.Telemetry.Extenstion;

var builder = WebApplication.CreateBuilder(args);

var gatewayOptions = builder.Configuration.Get<GatewayOptions>();
var seqOptions = builder.Configuration.GetSection(SeqOptions.Position).Get<SeqOptions>();
var telemetryOptions = builder.Configuration.GetSection(TelemetryOptions.Position).Get<TelemetryOptions>();

builder.AddCustomLog(options =>
{
    options.ApplicationName = gatewayOptions.ApplicationName;
    options.WriteToSeq = seqOptions.IsEnabled;
    options.SeqServerUrl = seqOptions.ServerUrl;
});

builder.Services.AddCustomTelemetry(options =>
{
    options.ApplicationName = gatewayOptions.ApplicationName;
    options.UseZipkin = telemetryOptions.UseZipkin;
    options.ZipkinEndpoint = telemetryOptions.ZipkinEndpoint;
});

var app = builder.Build();

try
{
    app.Logger.LogInformation("Starting Web Host [{ServiceName}]", gatewayOptions.ApplicationName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", gatewayOptions.ApplicationName);
}
finally
{
    Serilog.Log.CloseAndFlush();
}