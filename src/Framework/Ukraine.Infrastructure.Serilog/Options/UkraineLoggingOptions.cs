using Serilog.Events;

namespace Ukraine.Infrastructure.Serilog.Options;

public class UkraineLoggingOptions
{
	public string? ServiceName { get; set; }

	public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

	public Action<UkraineLoggingWriteOptions>? WriteTo { get; set; }

	internal Dictionary<string, LogEventLevel> OverrideDictionary { get; } = new();

	public void Override(Dictionary<string, LogEventLevel>? values)
	{
		if (values == null)
			return;

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