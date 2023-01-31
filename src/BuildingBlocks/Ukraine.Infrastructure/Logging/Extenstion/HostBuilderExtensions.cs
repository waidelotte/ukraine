using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.Logging.Options;

namespace Ukraine.Infrastructure.Logging.Extenstion;

public static class HostBuilderExtensions
{
    public static void AddCustomLog(this IHostBuilder hostBuilder, IConfiguration configuration, Action<CustomLogOptions> options)
    {
        var opt = new CustomLogOptions();
        options.Invoke(opt);
        
        if(string.IsNullOrEmpty(opt.ApplicationName)) throw CoreException.NullOrEmpty(nameof(opt.ApplicationName));
        
        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration);

        if (opt.WriteToConsole)
            loggerConfiguration.WriteTo.Console();

        if (opt.WriteToSeq)
        {
            if(string.IsNullOrEmpty(opt.SeqServerUrl)) throw CoreException.NullOrEmpty(nameof(opt.SeqServerUrl));
            loggerConfiguration.WriteTo.Seq(opt.SeqServerUrl);
        }

        loggerConfiguration.Enrich.WithProperty("ApplicationName", opt.ApplicationName);
            
        Log.Logger = loggerConfiguration.CreateLogger();

        if(opt.UseSerilog)
            hostBuilder.UseSerilog();
    }
}