using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.Logging.Options;

namespace Ukraine.Infrastructure.Logging.Extenstion;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseUkraineSerilog(this IHostBuilder hostBuilder, string serviceName, IConfigurationSection configurationSection)
    {
        return UseUkraineSerilog(hostBuilder, serviceName, options =>
        {
            options.MinimumLevel = configurationSection.GetValue(nameof(options.MinimumLevel), options.MinimumLevel);
            options.MinimumLevelOverride = configurationSection.GetSection(nameof(options.MinimumLevelOverride)).Get<Dictionary<string, LogEventLevel>?>();
            options.WriteTo = writeOptions =>
            {
                var writeSection = configurationSection.GetSection(nameof(options.WriteTo));
                writeOptions.WriteToConsole = writeSection.GetValue(nameof(writeOptions.WriteToConsole), writeOptions.WriteToConsole);
                writeOptions.WriteToSeqServerUrl = writeSection.GetValue<string?>(nameof(writeOptions.WriteToSeqServerUrl));
            };
        });
    }
    
    public static IHostBuilder UseUkraineSerilog(this IHostBuilder hostBuilder, string serviceName, 
        Action<UkraineLoggingOptions>? configure = null)
    {
        var options = new UkraineLoggingOptions();
        configure?.Invoke(options);

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Is(options.MinimumLevel);

        if (options.MinimumLevelOverride != null)
        {
            foreach (var minLevelOverride in options.MinimumLevelOverride)
            {
                loggerConfiguration.MinimumLevel.Override(minLevelOverride.Key, minLevelOverride.Value);
            }
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