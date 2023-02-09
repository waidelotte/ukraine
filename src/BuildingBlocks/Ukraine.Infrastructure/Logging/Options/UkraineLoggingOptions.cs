using Serilog.Events;

namespace Ukraine.Infrastructure.Logging.Options;

public class UkraineLoggingOptions
{
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
    public Action<UkraineLoggingWriteOptions>? WriteTo { get; set; }

    public Dictionary<string, LogEventLevel> OverrideDictionary = new();
    
    public void Override(Dictionary<string, LogEventLevel> values)
    {
        foreach (var value in values)
        {
            Override(value);
        }
    }
    
    public void Override(KeyValuePair<string, LogEventLevel> value)
    {
        Override(value.Key, value.Value);
    }
    
    public void Override(string key, LogEventLevel value)
    {
        OverrideDictionary.Add(key, value);
    }
}