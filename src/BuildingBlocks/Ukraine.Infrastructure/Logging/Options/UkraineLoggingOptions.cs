using Serilog.Events;

namespace Ukraine.Infrastructure.Logging.Options;

public class UkraineLoggingOptions
{
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
    public Dictionary<string, LogEventLevel>? MinimumLevelOverride { get; set; }
    public Action<UkraineLoggingWriteOptions>? WriteTo { get; set; }
}