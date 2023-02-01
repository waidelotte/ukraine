using Ukraine.Domain.Exceptions;
using Ukraine.Gateways.Http.Options;

var builder = WebApplication.CreateBuilder(args);

var gatewayOptions = builder.Configuration.Get<GatewayOptions>();
if (gatewayOptions == null) throw CoreException.Exception("Unable to initialize section: root");

var app = builder.Build();

try
{
    app.Logger.LogInformation("Starting Web Host [{ServiceName}]", gatewayOptions.ServiceName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", gatewayOptions.ServiceName);
}
finally
{
    Serilog.Log.CloseAndFlush();
}