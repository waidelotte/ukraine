using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Ukraine.Domain.Exceptions;

namespace Ukraine.Infrastructure.Logging;

public static class WebApplicationBuilderExtensions
{
    public static void AddCustomLog(this WebApplicationBuilder builder, Action<CustomLogOptions> options)
    {
        AddCustomLog(builder, builder.Configuration, options);
    }

    public static void AddCustomLog(this WebApplicationBuilder builder, IConfiguration configuration,
        Action<CustomLogOptions> options)
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
            builder.Host.UseSerilog();
    }
}