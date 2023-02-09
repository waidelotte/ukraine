using Microsoft.Extensions.Hosting;
using Serilog;
using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.Logging.Options;

namespace Ukraine.Infrastructure.Logging.Extenstion;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseUkraineSerilog(this IHostBuilder hostBuilder, string serviceName, 
        Action<UkraineLoggingOptions>? configure = null)
    {
        var options = new UkraineLoggingOptions();
        configure?.Invoke(options);

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Is(options.MinimumLevel);
        
        foreach (var minLevelOverride in options.OverrideDictionary)
        {
            loggerConfiguration.MinimumLevel.Override(minLevelOverride.Key, minLevelOverride.Value);
        }
        
        var writeOptions = new UkraineLoggingWriteOptions();
        options.WriteTo?.Invoke(writeOptions);

        if(writeOptions.WriteToConsole)
            loggerConfiguration.WriteTo.Console();
        
        if(!string.IsNullOrEmpty(writeOptions.WriteToSeqServerUrl))
            loggerConfiguration.WriteTo.Seq(writeOptions.WriteToSeqServerUrl);
        
        if(string.IsNullOrEmpty(serviceName)) throw CoreException.NullOrEmpty(nameof(serviceName));
        loggerConfiguration.Enrich.WithProperty(Constants.ENRICH_SERVICE_PROPERTY, serviceName);
            
        Log.Logger = loggerConfiguration.CreateLogger();

        hostBuilder.UseSerilog();

        return hostBuilder;
    }
}